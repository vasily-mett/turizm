using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using turizm.Lib.Classes;

namespace turizm.Lib.DB
{
    public class CommentDatabase : BaseDB
    {


        private readonly Options options;

        /// <summary>
        /// открывает базу данных, если файла не существует, то создает пустой
        /// </summary>
        /// <param name="options"></param>
        public CommentDatabase(Options options)
            : base(options.DatabaseFileName)
        {
            this.options = options;
            OpenDB();
        }



        /// <summary>
        /// обновляет базу данных, добавляет недостающие обсуждеия
        /// </summary>
        /// <param name="links">ссылки на обсуждения</param>
        internal void LoadTopics(List<Topic> topics)
        {
            foreach (Topic t in topics)
                AddTopic(t);
        }

        /// <summary>
        /// добавление строки в таблицу обсуждений 
        /// </summary>
        /// <param name="t"></param>
        private void AddTopic(Topic t)
        {
            if (!TopicExists(t.TopicID))
                Add(t);
        }

        /// <summary>
        /// проверка существует ли заданное обсуждение в БД
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        private bool TopicExists(long topicID)
        {
            string com = "SELECT * FROM '" + tb_topics + "' WHERE topic_id = '" + topicID+"';";
            List<Topic> tops = ExecuteTopicReader(com);
            return tops.Count > 0;
        }


        /// <summary>
        /// преобразование текстовых ссылок в массив объектов Topic
        /// </summary>
        /// <param name="links"></param>
        /// <returns></returns>
        public List<Topic> ParseTopicLinks(List<string> links)
        {
            List<Topic> res = new List<Topic>();

            foreach (string str in links)
            {
                //https://vk.com/topic-60394803_29506343
                string[] ids = str.Split('-');
                string[] ids1 = ids[1].Split('_');

                long group_id = long.Parse(ids1[0]);
                long topic_id = long.Parse(ids1[1]);
                res.Add(new Topic() { TopicID = topic_id, GroupID = group_id });
            }

            return res;

        }

        /// <summary>
        /// добавляет комментарии в базу данных
        /// </summary>
        /// <param name="new_comments"></param>
        internal void AddComments(List<Comment> new_comments)
        {
            foreach (Comment comm in new_comments)
                Add(comm);
        }

        /// <summary>
        /// Найти последний (по дате) комментарий в заданной теме
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal Comment GetLastComment(Topic t)
        {
            string com = "SELECT * FROM '" + tb_comments + "' WHERE topic_id = " + t.TopicID;
            List<Comment> comms = ExecuteCommentReader(com);
            Comment max = comms.Max();
            return max;
        }
    }
}
