using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.DB
{
    public class BaseDB
    {
        private string connectionString;
        private SQLiteConnection connection;
        private string FileName;

        /// <summary>
        /// открытие базы данных. Если бд не существует, то создаёт новую
        /// </summary>
        protected void OpenDB(string FileName)
        {
            this.FileName = FileName;
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
                @"CREATE TABLE tb_topics
                (topic_id INTEGER PRIMARY KEY NOT NULL, 
                name VARCHAR(255)
                );",
                con);
            commCreate.ExecuteNonQuery();

            commCreate.CommandText = @"CREATE TABLE tb_comments 
                (comment_id INTEGER PRIMARY KEY NOT NULL,
                user_id INTEGER,
                comment_text TEXT,
                comment_date DATE,
                likes INTEGER);";
            commCreate.ExecuteNonQuery();

            con.Close();
        }

    }
}
