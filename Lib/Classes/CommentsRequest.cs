using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    /// <summary>
    /// ответ сервера при запросе комментариев. (спосок коментов и список )
    /// </summary>
    public class CommentsResponse
    {
        public CommentsResponse()
        {
            Comments = new List<Comment>();
            Users = new List<User>();
        }

        /// <summary>
        /// список комментариев
        /// </summary>
        public List<Comment> Comments { get; set; }

        public int CountComents { get { return Comments.Count; } }

        /// <summary>
        /// список пользователей
        /// </summary>
        public List<User> Users { get; set; }

        internal void Add(CommentsResponse commentsResponse)
        {
            Comments.AddRange(commentsResponse.Comments);
            Users.AddRange(commentsResponse.Users);
        }

        internal void Add(Comment comment)
        {
            Comments.Add(comment);
        }

        internal void RemoveCommentAt(int v)
        {
            Comments.RemoveAt(v);
        }

        internal void Add(User user)
        {
            if (!Users.Contains(user))
            this.Users.Add(user);
        }
    }

}
