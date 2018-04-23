using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    /// <summary>
    /// ответ сервера при запросе комментариев. (спосок коментов и список пользователей)
    /// </summary>
    public class CommentsResponse
    {
        /// <summary>
        /// инициализация списков
        /// </summary>
        public CommentsResponse()
        {
            Comments = new List<Comment>();
            Users = new List<User>();
        }

        /// <summary>
        /// список комментариев
        /// </summary>
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Количество комментариев в списке
        /// </summary>
        public int CountComents { get { return Comments.Count; } }

        /// <summary>
        /// список пользователей
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Добавление нового списка в конец
        /// </summary>
        /// <param name="commentsResponse"></param>
        internal void Add(CommentsResponse commentsResponse)
        {
            Comments.AddRange(commentsResponse.Comments);
            Users.AddRange(commentsResponse.Users);
        }

        /// <summary>
        /// добавление одного комментария
        /// </summary>
        /// <param name="comment">объект - комментарий</param>
        internal void Add(Comment comment)
        {
            Comments.Add(comment);
        }

        /// <summary>
        /// удаление комментария на заданной позиции
        /// </summary>
        /// <param name="v">номер в списке комментариев</param>
        internal void RemoveCommentAt(int v)
        {
            Comments.RemoveAt(v);
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user">объект информации о пользователе</param>
        internal void Add(User user)
        {
            if (!Users.Contains(user))
            this.Users.Add(user);
        }
    }

}
