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

        /// <summary>
        /// создает новый экземпляр фильтра комментариев
        /// </summary>
        /// <param name="db">база данных с добавленными пользователями</param>
        public CommentPrefilter(CommentDatabase db)
        {
            this.db = db;
        }


        /// <summary>
        /// отсеивает комментарии для сохранения в БД
        /// </summary>
        /// <param name="new_comments">скачанные комментарии</param>
        /// <returns>комментарии, которые надо добавлять в БД</returns>
        internal List<Comment> Prefilter(List<Comment> new_comments)
        {
            List<Comment> res = new List<Comment>();

            foreach (Comment comm in new_comments)
                if (IsCommentMatch(comm))
                    res.Add(comm);

            return res;
        }

        /// <summary>
        /// проверяет подходит комментарий для добавления  в БД или нет
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
        /// <param name="comm"></param>
        /// <returns></returns>
        private VkObjectType CommentFrom(Comment comm)
        {
            long c = db.Count(BaseDB.tb_users, "WHERE user_id=" + comm.UserID.ToString());
            return c != 0 ? VkObjectType.User : VkObjectType.Group;
        }

        /// <summary>
        /// проверяет текст комментария на содержание ссылок
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private bool ContainsLink(Comment comm)
        {
            //TODO: Умненьшить нагрузку на проц. (заменить регулярку на что-то ещё, не такое прожорливое)
            
            bool res = RegExpressionLink.IsMatch(comm.Text);
            return res;
        }
    }
}
