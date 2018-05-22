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
            Topics = new List<string>()
            {
                //vk.com group_id      topic_id
                "https://vk.com/topic-14897324_24801958",
                "https://vk.com/topic-33445697_25808851",
                "https://vk.com/topic-40062539_35535029",
                "https://vk.com/topic-60394803_29506343",
                "https://vk.com/topic-42365455_29275795",
                "https://vk.com/topic-140629566_35003076",
                "https://vk.com/topic-61009220_29225653"
            };
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
