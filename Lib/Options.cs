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
    /// настройки приложения
    /// </summary>
    public class Options
    {
        /// <summary>
        /// создает объект настроек по умолчанию
        /// </summary>
        public Options()
        {
            ApplicationID = 6442889;
            DatabaseFileName = "db.sqlite";
            AccessToken = null;
            Topics = new List<string>()
            {
                //vk.com group_id     topic_id
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
        /// access token, полученный через браузер или через VKNet
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// список ссылок на обсуждения, которые будут просматриваться при обработке
        /// </summary>
        public List<string> Topics { get; set; }

        /// <summary>
        /// имя файла базы данных
        /// </summary>
        private string databaseFileName;

        /// <summary>
        /// имя файла базы данных
        /// </summary>
        public string DatabaseFileName { get { return Application.StartupPath + "\\" + databaseFileName; } set { databaseFileName = Path.GetFileName(value); } }
    }
}
