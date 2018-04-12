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
using turizm.Lib.Classes;
using VkNet.Utils;
using Newtonsoft.Json.Linq;

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
            /* 1. Добавить недостающие обсуждения
             * 2. Пройтись по всем обсуждениям и добавить недостающие комментарии 
             *      а. проверить последний добавленный комментарий (по дате написания комментария) (MAX date WHERE topic_id == t.TopicID)
             *      б. загрузить с сайта все коменты этого обсуждения, начиная с последнего добавленного (по частям)
             *      в. записать всё в БД              
             */
            List<Topic> topics = database.ParseTopicLinks(options.Topics);
            database.LoadTopics(topics);

            foreach (Topic t in topics)
            {
                Comment last_comm = database.GetLastComment(t);
                List<Comment> new_comments = GetCommentsFrom(t, last_comm);
                database.AddComments(new_comments);
            }
        }

        /// <summary>
        /// получить все комментарии в заданном обсуждении, начиная с заданного комментария
        /// </summary>
        /// <param name="t">обсуждение</param>
        /// <param name="last_comm">комментарий, с которого надо начать (не включая его)</param>
        /// <returns></returns>
        private List<Comment> GetCommentsFrom(Topic t, Comment last_comm)
        {
            if (t == null)
                throw new ArgumentException("Нельзя найти комментарии неопределенного обсуждения");

            string start_comment_id = "";
            if (last_comm == null)
                start_comment_id = "";
            else
                start_comment_id = "&start_comment_id="+last_comm.CommentID.ToString();

            List<Comment> res = new List<Comment>();

            string url = string.Format("https://api.vk.com/method/board.getComments?group_id={0}&topic_id={1}&need_likes=1{2}&count={3}&v=5.52",
                t.GroupID,
                t.TopicID,
                start_comment_id,
                100 //запрашиваем по 100 комментариев
                );
            JToken json = this.GetJson(url);
            JToken items = json["response"]["items"];
            foreach (JToken item in items)
            {
                long comment_id = long.Parse(item["id"].ToString());
                long from_id = long.Parse(item["from_id"].ToString());
                long date = long.Parse(item["date"].ToString());
                string text = item["text"].ToString();
                long likes = long.Parse(item["likes"]["count"].ToString());
                DateTime parsed_date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(date);
                res.Add(new Comment() { CommentID=comment_id,Likes=likes,Text=text,TopicID=t.TopicID,UserID=from_id,Date=parsed_date });
            }

            //если был задан стартовый элемент, то надо удалить, а то он два раз добавляется
            //да, ненаучно, но что делать)
            if (last_comm != null)
                res.RemoveAt(0);

            return res;
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
