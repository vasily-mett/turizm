using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    public class Comment
    {

        public long TopicID { get; set; }

        public long UserID { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public long Likes { get; set; }
           
    }
}
