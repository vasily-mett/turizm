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
    /// БД комментариев
    /// </summary>
    public class CommentDatabase : BaseDB
    {
        /// <summary>
        /// Общее количество комментариев в БД
        /// </summary>
        public long TotalComments
        {
            get
            {
                return base.Count(tb_comments);
            }
        }

        /// <summary>
        /// Общее количество пользователей в БД
        /// </summary>
        public long TotalUsers { get { return Count(tb_users); } }

        /// <summary>
        /// Открывает БД, если файла не существует, то создает пустой
        /// </summary>
        /// <param name="options"></param>
        public CommentDatabase(Options options)
            : base(options.DatabaseFileName)
        {
            OpenDB();
            LoadAdvertKw(options.AdvertKeywordsFileName);
        }

        /// <summary>
        /// Обновляет базу данных, добавляет недостающие обсуждеия
        /// </summary>
        /// <param name="links">ссылки на обсуждения</param>
        internal void LoadTopics(List<Topic> topics)
        {
            foreach (Topic t in topics)
                AddTopic(t);
        }

        /// <summary>
        /// Загрузить список рекламных слов
        /// </summary>
        /// <param name="advertKeywordsFileName">адрес файла с рекламными словами</param>
        public void LoadAdvertKw(string advertKeywordsFileName)
        {
            //Чтение файла
            StreamReader sr = new StreamReader(advertKeywordsFileName);
            List<AdvWord> lines = new List<AdvWord>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                lines.Add(new AdvWord() { Word = line });
            }
            sr.Close();

            //Заполнение БД
            this.ClearTable(tb_advert);
            foreach (AdvWord wd in lines)
                this.AddObject(wd);
        }


        /// <summary>
        /// Добавление строки в таблицу обсуждений 
        /// </summary>
        /// <param name="t"></param>
        private void AddTopic(Topic t)
        {
            if (t == null)
                return;
            if (!TopicExists(t.TopicID))
                AddObject(t);
        }

        /// <summary>
        /// Получить список рекламных слов 
        /// </summary>
        /// <returns></returns>
        internal List<AdvWord> GetAdvKeywords()
        {
            return base.ExecuteAdvertReader("SELECT * FROM " + tb_advert);
        }

        /// <summary>
        /// Проверка существует ли заданное обсуждение в БД
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
        /// Получить информацию о пользователе по заданному ID
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        /// <returns></returns>
        public User GetUser(long userID)
        {
            List<User> uss = base.ExecuteUserReader("SELECT * FROM '" + tb_users + "' WHERE user_id=" + userID);
            if (uss.Count > 0)
                return uss[0];
            else
                return null;
        }

        /// <summary>
        /// Преобразование текстовых ссылок в массив объектов Topic
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
        /// Поиск комментариев в БД
        /// </summary>
        /// <param name="find">слова, которые надо найти</param>
        /// <param name="exclude">слова, которые не должны попадаться в результате</param>
        /// <returns>список найденных комментариев</returns>
        public List<Comment> FindComments(List<string> find, List<string> exclude)
        {
            if (find == null)
                find = new List<string>();
            if (exclude == null)
                exclude = new List<string>();

            bool need_inc = find.Count > 0;
            bool need_exc = exclude.Count > 0;

            //запрос вида SELECT * FROM (блок включения слов) WHERE (блок исключения слов)
            //начало команды всегда одинаковое
            string command = "SELECT * FROM ";

            if (need_inc)   //Если есть, то искать, то добавляем блок включения слов
            {
                command += " ( ";
                command += @" SELECT * FROM '" + tb_comments + "' WHERE ";
                foreach (string fstr in find)
                    command += @" comment_text LIKE '%" + fstr + "%' OR ";

                command = command.Substring(0, command.Length - "OR ".Length);   //Удаляем последний OR
                command += ") ";   //Закрываем блок
            }
            else
                command += " '" + tb_comments + "'";   //Если нет, то берем всю таблицу

            if (need_exc)   //Если есть слова для исключения, то добавляем исключения
            {
                command += " WHERE ";
                foreach (string estr in exclude)
                    command += " comment_text NOT LIKE '%" + estr + "%'" + " AND ";

                command = command.Substring(0, command.Length - "AND ".Length);
            }
            command += ";";   //В любом случае, завершаем команду ';'

            List<Comment> comms = ExecuteCommentReader(command);
            return comms;
        }

        /// <summary>
        /// Добавление пользователей в БД
        /// </summary>
        /// <param name="users"></param>
        internal void AddUsers(List<User> users)
        {
            if (users == null || users.Count == 0)
                return;
            this.Add(users);
        }

        /// <summary>
        /// Добавляет комментарии в БД
        /// </summary>
        /// <param name="new_comments"></param>
        internal void AddComments(List<Comment> new_comments)
        {
            if (new_comments == null || new_comments.Count == 0)
                return;
            this.Add(new_comments);
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
