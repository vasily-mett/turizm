using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    /// <summary>
    /// структура информации об обсуждении
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// ID обсуждения
        /// </summary>
        public long TopicID { get; set; }

        /// <summary>
        /// название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID группы - владельца обсуждения
        /// </summary>
        public long GroupID { get; set; }
    }
}
