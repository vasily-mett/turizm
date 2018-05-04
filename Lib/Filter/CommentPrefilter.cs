using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using turizm.Lib.Classes;
using turizm.Lib.DB;
using VkNet;
using VkNet.Enums;

namespace turizm.Lib.Filter
{
    /// <summary>
    /// обработка комментариев до занесения в БД (отсев спама, коротких  коментов)
    /// </summary>
    class CommentPrefilter
    {
        private readonly CommentDatabase db;
        private readonly Regex RegExpressionLink = new Regex(@"w*(https?:\/\/)?([\w\.]+)\.([a-z]{2,6}\.?)(\/[\w\.]*)*\/?w*");
        List<AdvWord> words;

        /// <summary>
        /// создает новый экземпляр фильтра комментариев
        /// </summary>
        /// <param name="db">база данных с добавленными пользователями</param>
        public CommentPrefilter(CommentDatabase db)
        {
            this.db = db;
            words = db.GetAdvKeywords();
        }

        /// <summary>
        /// отсеивает комментарии для сохранения в БД и записывает количество рекламных слов в тексте комментария
        /// </summary>
        /// <param name="new_comments">скачанные комментарии</param>
        /// <returns>комментарии, которые надо добавлять в БД</returns>
        internal List<Comment> Prefilter(List<Comment> new_comments)
        {
            List<Comment> res = new List<Comment>();
            foreach (Comment comm in new_comments)
                if (IsCommentMatch(comm))
                    res.Add(PrepareComment(comm));
            return res;
        }

        /// <summary>
        /// подготовка комментария перед добавлением в БД (удаление ссылок на страницы ВК, посчет рекламных слов в тексте)
        /// </summary>
        /// <param name="comm">комментарий</param>
        /// <returns></returns>
        private Comment PrepareComment(Comment comm)
        {
            //удаление ссылок вк (ответы, упоминания)
            string text = comm.Text;
            text = Regex.Replace(text, @"\[[^\)]+\]", "");
            text = text.TrimStart(new char[2] { ',', ' ' });
            comm.Text = text;
            comm.AdvertWordsCount = CountAdvertWords(comm.Text);
            return comm;
        }

        /// <summary>
        /// возвращает количество рекламных слов в заданном тексте
        /// </summary>
        /// <param name="text">текст</param>
        /// <returns></returns>
        private int CountAdvertWords(string text)
        {
            int res = 0;
            foreach (AdvWord kw in words)
                if (checkMask(text,kw.Word))
                    res++;
            return res;

        }

        /// <summary>
        /// проверка текста на соответствие маске
        /// </summary>
        /// <param name="text"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        static public bool checkMask(string text, string mask)
        {
            string[] exts = mask.Split('|', ',', ';');
            string pattern = string.Empty;
            foreach (string ext in exts)
            {
                pattern += @"^";//признак начала строки
                foreach (char symbol in ext)
                    switch (symbol)
                    {
                        case '.': pattern += @"\."; break;
                        case '?': pattern += @"."; break;
                        case '*': pattern += @".*"; break;
                        default: pattern += symbol; break;
                    }
                pattern += @"$|";//признак окончания строки
            }
            if (pattern.Length == 0) return false;
            pattern = pattern.Remove(pattern.Length - 1);
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(text);
        }

        /// <summary>
        /// проверяет подходит комментарий для добавления в БД или нет
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private bool IsCommentMatch(Comment comm)
        {
            /* 1) Всё, что с ссылками - реклама.
             * 2) Всё, что не от имени пользователя, убираем.
             * 3) Всё, что меньше пяти слов, отсеиваем. 
             * 4) Всё, что вопрос, убираем.
             * 5) Количество символов > 30
             */

            //длина текста комментария
            if (comm.Text.Length < 30)
                return false;

            //содержит знак "?"
            if (comm.Text.Contains('?'))
                return false;

            //количество слов
            if (comm.Text.Split(' ').Length < 5)
                return false;

            //содержит ссылки
            if (ContainsLink(comm))
                return false;

            //тип автора - пользователь
            if (CommentFrom(comm) != VkObjectType.User)
                return false;

            return true;
        }

        /// <summary>
        /// проверяет автора комментария и возвращает тип
        /// </summary>
        /// <param name="comm">проверяемый комментарий</param>
        /// <returns></returns>
        private VkObjectType CommentFrom(Comment comm)
        {
            long c = db.Count(BaseDB.tb_users, "WHERE user_id=" + comm.UserID.ToString());
            return c != 0 ? VkObjectType.User : VkObjectType.Group;
        }

        /// <summary>
        /// проверяет текст комментария на содержание ссылок
        /// </summary>
        /// <param name="comm">проверяемый комментарий</param>
        /// <returns></returns>
        private bool ContainsLink(Comment comm)
        {
            //TODO: Уменьшить нагрузку на проц. (заменить регулярку на что-то ещё, не такое прожорливое)         
            bool res = RegExpressionLink.IsMatch(comm.Text);
            return res;
        }
    }
}
