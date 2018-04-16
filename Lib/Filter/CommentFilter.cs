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
using static turizm.Lib.VK.VK;

namespace turizm.Lib.Filter
{
    /// <summary>
    /// обработка комментариев до занесения в БД (отсев спама, коротких  коментов)
    /// </summary>
    class CommentPrefilter
    {
        private readonly VkApi api;
        private readonly CommentDatabase db;

        public CommentPrefilter(VkNet.VkApi api, CommentDatabase db)
        {
            this.api = api;
            this.db = db;
        }


        /// <summary>
        /// отсеивает комментарии для сохранения в БД
        /// </summary>
        /// <param name="new_comments"></param>
        /// <returns></returns>
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
        /// проверяет автора комментария. Если это обычный пользователь, то возвращает 
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private VkObjectType CommentFrom(Comment comm)
        {
            //долго работает
            //var v = api.Utils.ResolveScreenName("id" + comm.UserID);
            //return v.Type;

            long c = db.Count(CommentDatabase.tb_users, "WHERE user_id=" + comm.UserID.ToString());
            return c != 0 ? VkObjectType.User : VkObjectType.Group;
        }

        /// <summary>
        /// проверяет текст комментария на содержание ссылок
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        private bool ContainsLink(Comment comm)
        {
            Regex reg = new Regex(@"w*(https?:\/\/)?([\w\.]+)\.([a-z]{2,6}\.?)(\/[\w\.]*)*\/?w*");
            bool res = reg.IsMatch(comm.Text);
            return res;
        }
    }
}
