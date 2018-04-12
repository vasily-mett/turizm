using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace turizm.Lib
{
    public class Options
    {
        /// <summary>
        /// создает объект настроек по умолчанию
        /// </summary>
        public Options()
        {
            ApplicationID = 6442889;
            AccessToken = "6d9e2282cf9749603c8f6b4b010f300b0e7ef9a20faff87f592db94d8b8c25c3188ccac5fdbe32cdba9f4";
            DatabaseFileName = "db.sqlite";
            Topics = new List<string>()
            {
                //vk.com group_id     topic_id
                "https://vk.com/topic-14897324_24801958",
                "https://vk.com/topic-33445697_25808851",
                "https://vk.com/topic-40062539_35535029",
                "https://vk.com/topic-60394803_29506343"
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
