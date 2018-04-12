using System;
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
    /// базовый класс взаимодействия с БД
    /// </summary>
    public class BaseDB
    {

        private string connectionString;
        private readonly SQLiteConnection connection;
        private readonly string FileName;
        protected const string tb_topics = "tb_topics";
        protected const string tb_comments = "tb_comments";
        private readonly string databaseDirectory;

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
        }

        /// <summary>
        /// открытие базы данных. Если бд не существует, то создаёт новую
        /// </summary>
        protected void OpenDB()
        {
            connectionString = "Data Source = " + this.FileName;

            if (!File.Exists(this.FileName))
                CreateDB();

        }

        /// <summary>
        /// создание пустой базы данных
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
                (comment_id INTEGER PRIMARY KEY NOT NULL,
                user_id INTEGER,
                topic_id INTEGER,
                comment_text TEXT,
                comment_date INTEGER,
                comment_likes INTEGER);";
            commCreate.ExecuteNonQuery();

            con.Close();
        }

        /// <summary>
        /// добавление новой строки в базу 
        /// </summary>
        /// <param name="lat">широта</param>
        /// <param name="lon">долгота</param>
        /// <param name="alt">высота</param>
        /// <param name="addr">адрес</param>
        protected void Add(object obj)
        {
            string com = "";
            if (obj is Comment)
            {
                //добавление комментария в таблицу
                Comment comment = obj as Comment;
                long date = (int)(comment.Date - new DateTime(1970, 1, 1)).TotalSeconds;
                com = string.Format("INSERT INTO '" + tb_comments + @"' ('comment_id','user_id','topic_id','comment_text','comment_date','comment_likes') VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');",
                    comment.CommentID,
                    comment.UserID,
                    comment.TopicID,
                    comment.Text,
                    date,
                    comment.Likes
                    );
            }
            else if (obj is Topic)
            {
                //добавление обсуждения в таблицу
                Topic topic = obj as Topic;
                com = string.Format("INSERT INTO '" + tb_topics + @"' ('topic_id','group_id','name') VALUES ('{0}','{1}','{2}');",
                    topic.TopicID,
                    topic.GroupID,
                    string.IsNullOrEmpty(topic.Name) ? "noname" : topic.Name
                    );
            }
            ExecuteQuery(com);

            List<Comment> test = ExecuteCommentReader("SELECT * FROM 'tb_comments'");
        }


        /// <summary>
        /// выполнение запроса без результата
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected int ExecuteQuery(string query)
        {
            lock (connection)
            {
                connection.Open();
                SQLiteCommand com = connection.CreateCommand();
                com.CommandText = query;
                int i = com.ExecuteNonQuery();
                connection.Close();
                return i;
            }
        }

        /// <summary>
        /// выполнение запроса с результатом
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        protected List<Comment> ExecuteCommentReader(string com)
        {
            lock (connection)
            {
                connection.Open();
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
                    long likes =dr["comment_likes"] is DBNull ? 0 : Convert.ToInt64(dr["comment_likes"]);
                    res.Add(new Comment()
                    {
                        CommentID = com_id,
                        UserID = user_id ,
                        TopicID = topic_id,
                        Text = text,
                        Date = parsed_date,
                        Likes = likes
                    });
                }

                connection.Close();
                return res;
            }
        }


        /// <summary>
        /// выполнение запроса с результатом
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        protected List<Topic> ExecuteTopicReader(string com)
        {
            lock (connection)
            {
                connection.Open();
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

                connection.Close();
                return res;
            }
        }

    }
}
