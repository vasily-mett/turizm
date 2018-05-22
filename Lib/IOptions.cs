using System.Collections.Generic;

namespace turizm.Lib
{
    public interface IOptions
    {
        /// <summary>
        /// access token, полученный через браузер или через VKNet
        /// </summary>
        string AccessToken { get; set; }
        /// <summary>
        /// адрес файла с рекламными словами
        /// </summary>
        string AdvertKeywordsFileName { get; set; }
        /// <summary>
        /// ID приложения вконтакте
        /// </summary>
        ulong ApplicationID { get; set; }
        /// <summary>
        /// имя файла БД
        /// </summary>
        string DatabaseFileName { get; set; }
        /// <summary>
        /// список ссылок на обсуждения, которые будут просматриваться при обработке
        /// </summary>
        List<string> Topics { get; set; }
    }
}