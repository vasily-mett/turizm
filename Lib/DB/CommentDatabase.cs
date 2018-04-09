using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace turizm.Lib.DB
{
    public class CommentDatabase:BaseDB
    {
        private readonly Options options;
        
        
        public CommentDatabase(Options options)
        {
            this.options = options;
            OpenDB(options.DatabaseFileName);
        }

       

        /// <summary>
        /// обновляет базу данных, добавляет недостающие обсуждеия и комментарии
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="callback"></param>
        internal void LoadTopics(List<string> topics, Action<int> callback)
        {


        }
    }
}
