using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace turizm.Lib
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class Options : IOptions
    {
        /// <summary>
        /// Создает объект настроек по умолчанию
        /// </summary>
        public Options()
        {
            ApplicationID = 6442889;
            DatabaseFileName = "Turizm.sqlite";
            AdvertKeywordsFileName = "advert.txt";
            AccessToken = null;

            //загрузка ссылок на обсуждения из файла
            Topics = new List<string>();
            StreamReader sr = new StreamReader(Application.StartupPath + "\\topics.txt");
            while (!sr.EndOfStream)
                Topics.Add(sr.ReadLine());
            sr.Close();
        }

        /// <summary>
        /// ID приложения вконтакте
        /// </summary>
        public ulong ApplicationID { get; set; }

        /// <summary>
        /// Access token, полученный через браузер или через VKNet
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Список ссылок на обсуждения, которые будут просматриваться при обработке
        /// </summary>
        public List<string> Topics { get; set; }

        /// <summary>
        /// Имя файла БД
        /// </summary>
        private string databaseFileName;

        /// <summary>
        /// Адрес файла базы данных (прописывается весь путь)
        /// </summary>
        public string DatabaseFileName { get { return Application.StartupPath + "\\" + databaseFileName; } set { databaseFileName = Path.GetFileName(value); } }

        /// <summary>
        /// Имя файла с рекламными словами
        /// </summary>
        private string advertKeywordsFileName;

        /// <summary>
        /// Адрес файла с рекламными словами (прописывается весь путь)
        /// </summary>
        public string AdvertKeywordsFileName { get { return Application.StartupPath + "\\" + advertKeywordsFileName; } set { advertKeywordsFileName = Path.GetFileName(value); } }
    }
}
