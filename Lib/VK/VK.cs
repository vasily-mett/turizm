using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using VkNet.Categories;
using turizm.Lib.DB;

namespace turizm.Lib.VK
{
    /// <summary>
    /// оболочка АPI контакта
    /// </summary>
    public class VK : BaseHttp
    {
        private readonly VkApi api;
        private readonly Options options;

        /// <summary>
        /// конструктор с настройками
        /// </summary>
        /// <param name="options"></param>
        public VK(Options options)
        {
            this.options = options;
            api = new VkApi();
            ApiAuthParams par = new ApiAuthParams
            {
                AccessToken = options.AccessToken
            };
            api.Authorize(par);
        }

        /// <summary>
        /// обновление базы данных комментариев 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        public void UpdateDB(CommentDatabase database, Options options, Action<int> callback)
        {
           database.LoadTopics(options.Topics, callback);

        }

        /// <summary>
        /// получение access_token для сессии
        /// </summary>
        /// <returns></returns>
        private string GetToken()
        {
            string url = string.Format("https://oauth.vk.com/authorize?client_id={3}&display={0}&redirect_uri={1}&response_type=token&v={2}&scope={4}",
                "page",
                @"https://oauth.vk.com/blank.html",
                VkApi.VkApiVersion,
                options.ApplicationID,
                "offline"
                );
            Process.Start(url);


            return "";
        }
    }
}
