using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    public class Comment: IComparable
    {

        public long TopicID { get; set; }

        public long UserID { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public long Likes { get; set; }

        public long CommentID { get;  set; }

        public int CompareTo(object obj)
        {
            if (!(obj is Comment))
                throw new InvalidCastException("Тип должен быть Comment");
            return Date.CompareTo((obj as Comment).Date);
        }
    }
}
