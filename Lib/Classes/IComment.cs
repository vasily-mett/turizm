using System;
using System.Collections.Generic;

namespace turizm.Lib.Classes
{
    public interface IComment
    {
        /// <summary>
        /// рекламные маски
        /// </summary>
        List<string> AdvertMasks { get; set; }
        /// <summary>
        /// ID комментария
        /// </summary>
        long CommentID { get; set; }
        /// <summary>
        /// дата написания комментария
        /// </summary>
        DateTime Date { get; set; }
        /// <summary>
        /// количество лайков у комментария
        /// </summary>
        int Likes { get; set; }
        /// <summary>
        /// текст комментария
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// ID обсуждения
        /// </summary>
        long TopicID { get; set; }
        /// <summary>
        /// ID пользователя - автора комментария
        /// </summary>
        long UserID { get; set; }
        /// <summary>
        /// сравнение для поиска максимального элемента
        /// </summary>
        /// <param name="obj">комментарий</param>
        int CompareTo(object obj);
    }
}