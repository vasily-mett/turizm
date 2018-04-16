using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using turizm.Lib.Classes;
using VkNet.Enums;

namespace turizm.Lib.DB
{
    /// <summary>
    /// база данных комментариев
    /// </summary>
    public class CommentDatabase : BaseDB
    {


        private readonly Options options;

        /// <summary>
        /// общее количество комментариев в базе данных
        /// </summary>
        public long TotalComments { get {
                return base.Count(tb_comments);
            } }

        /// <summary>
        /// общее количество пользователей в БД
        /// </summary>
        public long TotalUsers { get { return Count(tb_users); } }

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
            if (t == null)
                return;
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
            string com = "SELECT * FROM '" + tb_topics + "' WHERE topic_id = '" + topicID + "';";
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

        internal void AddUsers(List<User> users, Action<string> callback)
        {
            if (users == null || users.Count == 0)
                return;
            this.Add(users, callback);
        }




        /// <summary>
        /// добавляет комментарии в базу данных
        /// </summary>
        /// <param name="new_comments"></param>
        internal void AddComments(List<Comment> new_comments, Action<string> callback)
        {
            if (new_comments == null || new_comments.Count == 0)
                return;
            this.Add(new_comments, callback);
        }

        

        /// <summary>
        /// Найти последний (по дате) комментарий в заданной теме
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal Comment GetLastComment(Topic t)
        {
            string com = "SELECT * FROM '" + tb_comments + "' WHERE comment_date = (SELECT MAX(comment_date) FROM '" + tb_comments + "' WHERE topic_id=" + t.TopicID + @")";
            List<Comment> comms = ExecuteCommentReader(com);
            Comment max = comms.Max();
            return max;
        }
    }
}
