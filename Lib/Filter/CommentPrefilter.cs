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
    /// Обработка комментариев до занесения в БД (отсев спама, коротких комментов)
    /// </summary>
    public class CommentPrefilter : ICommentPrefilter
    {
        private readonly CommentDatabase db;
        private readonly Regex RegExpressionLink = new Regex(@"w*(https?:\/\/)?([\w\.]+)\.([a-z]{2,6}\.?)(\/[\w\.]*)*\/?w*"); //регулярное выражение для ссылки
        List<AdvWord> words;

        /// <summary>
        /// Создает новый экземпляр фильтра комментариев
        /// </summary>
        /// <param name="db">база данных с добавленными пользователями</param>
        public CommentPrefilter(CommentDatabase db)
        {
            this.db = db;
            words = db.GetAdvKeywords();
        }

        /// <summary>
        /// Отсеивает комментарии для сохранения в БД и записывает количество рекламных слов в тексте комментария
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
        /// Подготовка комментария перед добавлением в БД (удаление ссылок на страницы ВК, посчет рекламных слов в тексте)
        /// </summary>
        /// <param name="comm">комментарий</param>
        /// <returns></returns>
        private Comment PrepareComment(Comment comm)
        {
            //Удаление ссылок вк (ответы, упоминания)
            string text = comm.Text;
            text = Regex.Replace(text, @"\[[^\)]+\]", "");
            text = text.TrimStart(new char[2] { ',', ' ' });
            comm.Text = text;
            return comm;
        }

        /// <summary>
        /// Для каждого комментария в списке записывает список рекламных масок, под которые подходит текст
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        public List<Comment> CountAdvertWords(List<Comment> comments)
        {
            foreach (Comment comm in comments)
                comm.AdvertMasks = CountAdvertWords(comm.Text);
            return comments;
        }

        /// <summary>
        /// Возвращает список рекламных масок в заданном тексте
        /// </summary>
        /// <param name="text">текст</param>
        /// <returns></returns>
        private List<string> CountAdvertWords(string text)
        {
            List<string> res = new List<string>();
            foreach (AdvWord kw in words)
                if (new Regex(kw.Word).IsMatch(text))
                    res.Add(kw.Word);
            return res;
        }

        /// <summary>
        /// Проверяет подходит комментарий для добавления в БД или нет
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private bool IsCommentMatch(Comment comm)
        {
            /* 
             * 1) Всё, что с ссылками - реклама.
             * 2) Всё, что не от имени пользователя, убираем.
             * 3) Всё, что меньше пяти слов, отсеиваем. 
             * 4) Всё, что вопрос, убираем.
             * 5) Количество символов > 30
             */

            //Длина текста комментария
            if (comm.Text.Length < 30)
                return false;

            //Содержит знак "?"
            if (comm.Text.Contains('?'))
                return false;

            //Количество слов
            if (comm.Text.Split(' ').Length < 5)
                return false;

            //Содержит ссылки
            if (ContainsLink(comm))
                return false;

            //Тип автора - пользователь
            if (CommentFrom(comm) != VkObjectType.User)
                return false;
            return true;
        }

        /// <summary>
        /// Проверяет автора комментария и возвращает тип
        /// </summary>
        /// <param name="comm">проверяемый комментарий</param>
        /// <returns></returns>
        private VkObjectType CommentFrom(Comment comm)
        {
            long c = db.Count(BaseDB.tb_users, "WHERE user_id=" + comm.UserID.ToString());
            return c != 0 ? VkObjectType.User : VkObjectType.Group;
        }

        /// <summary>
        /// Проверяет текст комментария на содержание ссылок
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
