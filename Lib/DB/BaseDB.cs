﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turizm.Lib.Classes;

namespace turizm.Lib.DB
{
    /// <summary>
    /// Базовый класс взаимодействия с БД
    /// </summary>
    public class BaseDB
    {
        private string connectionString;
        private readonly SQLiteConnection connection;
        private readonly string FileName;
        private readonly string databaseDirectory;
        public const string tb_topics = "tb_topics";
        public const string tb_comments = "tb_comments";
        public const string tb_users = "tb_users";
        public const string tb_advert = "tb_advert";

        /// <summary>
        /// Подсчет количества строк в заданной таблице
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        internal long Count(string tb)
        {
            return Count(tb, "");
        }

        /// <summary>
        /// Возвращает количество строк в заданной таблице и с заданным условием
        /// </summary>
        /// <param name="tb_users">таблица</param>
        /// <param name="condition">условие "WHERE ..."</param>
        /// <returns></returns>
        internal long Count(string tb, string condition)
        {
            string com = "SELECT COUNT(1) FROM '" + tb + "' " + condition;
            return long.Parse(ExecuteSingle(com));
        }

        /// <summary>
        /// создает базу данных или открывает готовую
        /// </summary>
        /// <param name="databaseFileName"></param>
        protected BaseDB(string databaseFileName)
        {
            databaseDirectory = Path.GetDirectoryName(databaseFileName);

            FileName = databaseFileName;
            connectionString = "Data Source = " + FileName;

            if (!File.Exists(FileName))
                CreateDB();

            connection = new SQLiteConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
        }

        /// <summary>
        /// Открытие БД. Если БД не существует, то создаёт новую
        /// </summary>
        protected void OpenDB()
        {
            connectionString = "Data Source = " + this.FileName;

            if (!File.Exists(this.FileName))
                CreateDB();
        }

        /// <summary>
        /// Закрывает БД
        /// </summary>
        public void Close()
        {
            connection.Close();
        }

        /// <summary>
        /// Создание пустой БД
        /// </summary>
        /// <param name="databaseFileName"></param>
        protected void CreateDB()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(this.FileName));
            SQLiteConnection.CreateFile(this.FileName);
            SQLiteConnection con = new SQLiteConnection("Data Source = " + this.FileName);
            con.Open();
            SQLiteCommand commCreate = new SQLiteCommand(
                @"CREATE TABLE " + tb_topics + @"
                (topic_id INTEGER PRIMARY KEY NOT NULL, 
                group_id INTEGER,
                name VARCHAR(255)
                );",
                con);
            commCreate.ExecuteNonQuery();

            commCreate.CommandText = @"CREATE TABLE " + tb_comments + @" 
                (comment_id INTEGER NOT NULL,
                user_id INTEGER NOT NULL,
                topic_id INTEGER NOT NULL,
                comment_text TEXT,
                comment_date INTEGER,
                comment_advkw INTEGER,
                comment_likes INTEGER);";
            commCreate.ExecuteNonQuery();

            commCreate.CommandText = @"CREATE TABLE " + tb_users + @" 
                (user_id INTEGER PRIMARY KEY NOT NULL,
                first_name TEXT,
                last_name TEXT);";
            commCreate.ExecuteNonQuery();

            commCreate.CommandText = @"CREATE TABLE " + tb_advert + @" 
                (id INTEGER PRIMARY KEY AUTOINCREMENT,
                word TEXT,
                hash TEXT);";
            commCreate.ExecuteNonQuery();

            con.Close();
        }

        /// <summary>
        /// Добавление новой строки в базу 
        /// </summary>
        /// <param name="lat">широта</param>
        /// <param name="lon">долгота</param>
        /// <param name="alt">высота</param>
        /// <param name="addr">адрес</param>
        protected void AddObject(object obj)
        {
            string com = "";
            if (obj is Comment)
            {
                //Добавление комментария в таблицу
                Comment comment = obj as Comment;
                comment.Text = comment.Text.Replace("'", "");
                long date = (int)(comment.Date - new DateTime(1970, 1, 1)).TotalSeconds;
                com = string.Format("INSERT INTO '" + tb_comments + @"' ('comment_id','user_id','topic_id','comment_text','comment_date','comment_likes') VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');",
                comment.CommentID,
                comment.UserID,
                comment.TopicID,
                comment.Text.ToLower(),
                date,
                comment.Likes);
            }
            else if (obj is Topic)
            {
                //Добавление обсуждения в таблицу
                Topic topic = obj as Topic;
                com = string.Format("INSERT INTO '" + tb_topics + @"' ('topic_id','group_id','name') VALUES ('{0}','{1}','{2}');",
                    topic.TopicID,
                    topic.GroupID,
                    string.IsNullOrEmpty(topic.Name) ? "noname" : topic.Name
                    );
            }
            else if (obj is AdvWord)
            {
                //Добавление рекламного слова в таблицу
                AdvWord word = obj as AdvWord;
                com = string.Format("INSERT INTO '" + tb_advert + @"' ('word','hash') VALUES ('{0}','{1}');",
                    word.Word,
                    word.Hash
                    );
            }
            ExecuteQuery(com);
        }

        /// <summary>
        /// Добавление многих комментариев в БД
        /// </summary>
        /// <param name="comments"></param>
        protected void Add(List<Comment> comments)
        {
            SQLiteTransaction trans = this.connection.BeginTransaction();
            for (int i = 0; i < comments.Count; i++)
            {
                SQLiteCommand cm = connection.CreateCommand();

                comments[i].Text = comments[i].Text.Replace("'", "");
                long date = (int)(comments[i].Date - new DateTime(1970, 1, 1)).TotalSeconds;
                string com = string.Format("INSERT INTO '" + tb_comments + @"' ('comment_id','user_id','topic_id','comment_text','comment_date','comment_likes') VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');",
                comments[i].CommentID,
                comments[i].UserID,
                comments[i].TopicID,
                comments[i].Text.ToLower(),
                date,
                comments[i].Likes
                );

                cm.CommandText = com;
                cm.ExecuteNonQuery();
            }
            trans.Commit();

        }

        /// <summary>
        /// Добавление пользователей в БД
        /// </summary>
        /// <param name="users">список пользователей для добавления</param>
        protected void Add(List<User> users_param)
        {
            //Подготовка списка (убираем повторяющихся пользователей)
            List<long> ids = new List<long>();
            List<User> uss = ExecuteUserReader("SELECT * FROM " + tb_users);
            List<User> users = new List<User>();
            foreach (User us in uss)
                ids.Add(us.UserID);
            for (int i = 0; i < users_param.Count; i++)
                if (!ids.Contains(users_param[i].UserID))
                {
                    ids.Add(users_param[i].UserID);
                    users.Add(users_param[i]);
                }

            if (users.Count == 0)
                return;

            //Добавление в БД
            SQLiteTransaction trans = this.connection.BeginTransaction();
            for (int i = 0; i < users.Count; i++)
            {
                SQLiteCommand cm = connection.CreateCommand();
                string com = string.Format("INSERT INTO '" + tb_users + @"' ('user_id','first_name','last_name') VALUES ('{0}','{1}','{2}');",
                users[i].UserID,
                users[i].FirstName.Replace("\'", ""),
                users[i].LastName.Replace("\'", ""));
                cm.CommandText = com;
                cm.ExecuteNonQuery();
            }
            trans.Commit();
        }

        /// <summary>
        /// Выполнение запроса без результата
        /// </summary>
        /// <param name="query">команда SQL</param>
        /// <returns></returns>
        protected int ExecuteQuery(string query)
        {
            SQLiteCommand com = connection.CreateCommand();
            com.CommandText = query;
            int i = com.ExecuteNonQuery();
            return i;
        }

        /// <summary>
        /// Выполнение запроса с результатом в виде списка комментариев
        /// </summary>
        /// <param name="com">команда SQL</param>
        /// <returns></returns>
        protected List<Comment> ExecuteCommentReader(string com)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = com;
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<Comment> res = new List<Comment>();

            while (dr.Read())
            {
                long d = dr["comment_date"] is DBNull ? 0 : Convert.ToInt64(dr["comment_date"]);
                DateTime parsed_date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(d);
                long com_id = Convert.ToInt64(dr["comment_id"]);
                long user_id = Convert.ToInt64(dr["user_id"]);
                long topic_id = Convert.ToInt64(dr["topic_id"]);
                string text = dr["comment_text"] is DBNull ? "" : dr["comment_text"].ToString();
                int likes = dr["comment_likes"] is DBNull ? 0 : Convert.ToInt32(dr["comment_likes"]);
                res.Add(new Comment()
                {
                    CommentID = com_id,
                    UserID = user_id,
                    TopicID = topic_id,
                    Text = text,
                    Date = parsed_date,
                    Likes = likes
                });
            }

            return res;

        }

        /// <summary>
        /// Выполнение запроса с результатом в виде списка пользователей
        /// </summary>
        /// <param name="com">команда SQL</param>
        /// <returns></returns>
        protected List<User> ExecuteUserReader(string com)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = com;
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<User> res = new List<User>();

            while (dr.Read())
            {
                long user_id = Convert.ToInt64(dr["user_id"]);

                string lname = dr["last_name"] is DBNull ? "" : dr["last_name"].ToString();
                string fname = dr["first_name"] is DBNull ? "" : dr["first_name"].ToString();
                res.Add(new User()
                {
                    FirstName = fname,
                    LastName = lname,
                    UserID = user_id
                });
            }
            return res;
        }

        /// <summary>
        /// Выполнение запроса с результатом в виде списка обсуждений
        /// </summary> 
        /// <param name="com">команда SQL</param>
        /// <returns></returns>
        protected List<Topic> ExecuteTopicReader(string com)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = com;
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<Topic> res = new List<Topic>();

            while (dr.Read())
            {
                long gr = Convert.ToInt64(dr["group_id"]);
                long ti = Convert.ToInt64(dr["topic_id"]);
                string nm = dr["name"] is DBNull ? "" : dr["name"].ToString();
                res.Add(new Topic()
                {
                    GroupID = gr,
                    TopicID = ti,
                    Name = nm
                });
            }
            return res;
        }

        /// <summary>
        /// Выполнение запроса с результатом в виде списка рекламных слов
        /// </summary>
        /// <param name="com">команда SQL</param>
        /// <returns></returns>
        internal List<AdvWord> ExecuteAdvertReader(string com)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = com;
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<AdvWord> res = new List<AdvWord>();

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string word = dr["word"] is DBNull ? "" : dr["word"].ToString();
                res.Add(new AdvWord()
                {
                    ID = id,
                    Word = word
                });
            }
            return res;
        }

        /// <summary>
        /// Выполнить запрос с одним результатом
        /// </summary>
        /// <param name="com">команда SQL</param>
        /// <returns></returns>
        protected string ExecuteSingle(string com)
        {
            bool need_close = false;

            //Открываем соединение, если надо
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                need_close = true;
            }
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = com;
            SQLiteDataReader dr = cmd.ExecuteReader();
            bool ff = dr.Read();
            string res = "";
            if (ff)
            {
                string resA = dr[0].ToString();
                res = resA.ToString();
            }

            //Если открывали соединение, то надо закрыть
            if (need_close)
                connection.Close();
            return res;
        }

        /// <summary>
        /// Очистка заданной таблицы
        /// </summary>
        /// <param name="table">имя таблицы</param>
        protected void ClearTable(string table)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM " + table;
            cmd.ExecuteNonQuery();
        }
    }
}
