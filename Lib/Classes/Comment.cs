using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    /// <summary>
    /// Структура информации о комментарии
    /// </summary>
    public class Comment : IComment
    {
        public Comment()
        {
            this.AdvertMasks = new List<string>();
        }

        /// <summary>
        /// ID обсуждения
        /// </summary>
        public long TopicID { get; set; }

        /// <summary>
        /// ID пользователя - автора комментария
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата написания комментария
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Количество лайков у комментария
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// ID комментария
        /// </summary>
        public long CommentID { get; set; }

        /// <summary>
        /// Количество рекламных масок в комментарии
        /// </summary>
        public List<string> AdvertMasks { get; set; }

        /// <summary>
        /// Сравнение для поиска максимального элемента
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Comment))
                throw new InvalidCastException("Тип должен быть Comment");
            return Date.CompareTo((obj as Comment).Date);
        }
    }
}
