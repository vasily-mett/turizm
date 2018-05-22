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
using turizm.Lib.Filter;
using System.Windows.Forms;

namespace turizm.Lib.VK
{
    /// <summary>
    /// оболочка АPI контакта
    /// </summary>
    public class VK : BaseHttp, IVK
    {
        /// <summary>
        /// апи контакта
        /// </summary>
        private readonly VkApi api;
        private readonly Options options;
        private readonly CommentPrefilter prefilter;
        private readonly CommentDatabase db;

        /// <summary>
        /// Конструктор с настройками
        /// </summary>
        /// <param name="options"></param>
        /// <param name="db"></param>
        public VK(Options options, CommentDatabase db)
        {
            this.options = options;
            this.db = db;
            api = new VkApi();
            db.LoadAdvertKw(options.AdvertKeywordsFileName);
            prefilter = new CommentPrefilter(db);
            options.AccessToken = GetToken(options.ApplicationID);
            ApiAuthParams par = new ApiAuthParams
            {
                AccessToken = options.AccessToken
            };
            api.Authorize(par);   //Подключение
        }

        /// <summary>
        /// Обновление БД комментариев 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="options"></param>
        public void UpdateDB(CommentDatabase database, Options options, Label labelProgress)
        {
            /* 
             * 1. Добавить недостающие обсуждения
             * 2. Пройтись по всем обсуждениям и добавить недостающие комментарии 
             *      а. проверить последний добавленный комментарий (по дате написания комментария) (MAX date WHERE topic_id == t.TopicID)
             *      б. загрузить с сайта все коменты этого обсуждения, начиная с последнего добавленного (по частям)
             *      в. записать всё в БД              
             */
            List<Topic> topics = database.ParseTopicLinks(options.Topics);
            database.LoadTopics(topics);

            for (int i = 0; i < topics.Count; i++)
            {
                Topic t = topics[i];   //Текущее обсуждение
                Comment last_comm = database.GetLastComment(t);
                CommentsResponse response = GetCommentsFrom(t, last_comm, (perc) =>
                {
                    string status = "Обработка обсуждения " + (i + 1) + "/" + topics.Count + ", завершено " + perc.ToString("0.0") + "%";
                    labelProgress.Text = status;
                    Application.DoEvents();
                });

                labelProgress.Text = "Обработка обсуждения " + (i + 1) + "/" + topics.Count + ", обработка результатов"; Application.DoEvents();
                database.AddUsers(response.Users);

                List<Comment> filtered_comments = prefilter.Prefilter(response.Comments);
                database.AddComments(filtered_comments);
            }
        }

        /// <summary>
        /// Получить все комментарии в заданном обсуждении, начиная с заданного комментария
        /// https://vk.com/dev/board.getComments
        /// </summary>
        /// <param name="t">обсуждение</param>
        /// <param name="last_comm">комментарий, с которого надо начать (не включая его)</param>
        /// <returns></returns>
        private CommentsResponse GetCommentsFrom(Topic t, Comment last_comm, Action<double> callback)
        {
            if (t == null)
                throw new ArgumentException("Нельзя найти комментарии неопределенного обсуждения");

            string start_comment_id = "";
            if (last_comm == null)
                start_comment_id = "";
            else
                start_comment_id = "&start_comment_id=" + last_comm.CommentID.ToString();

            CommentsResponse res = new CommentsResponse();

            string url = string.Format("https://api.vk.com/method/board.getComments?group_id={0}&topic_id={1}&need_likes=1{2}&count={3}&extended=1&v=5.52",
                t.GroupID,
                t.TopicID,
                start_comment_id,
                100   //Запрашиваем по 100 комментариев
                );
            JToken json = this.GetJson(url);

            //Добавление комментариев
            JToken items = json["response"]["items"];
            foreach (JToken item in items)
            {
                long comment_id = long.Parse(item["id"].ToString());
                long from_id = long.Parse(item["from_id"].ToString());
                long date = long.Parse(item["date"].ToString());
                string text = item["text"].ToString();
                int likes = int.Parse(item["likes"]["count"].ToString());
                DateTime parsed_date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(date);
                res.Add(new Comment() { CommentID = comment_id, Likes = likes, Text = text, TopicID = t.TopicID, UserID = from_id, Date = parsed_date });
            }

            //Добавление пользователей
            JToken profiles = json["response"]["profiles"];
            foreach (JToken profile in profiles)
            {
                long id = long.Parse(profile["id"].ToString());
                string fname = profile["first_name"].ToString();
                string lname = profile["last_name"].ToString();
                res.Add(new User() { FirstName = fname, LastName = lname, UserID = id });
            }

            //Общее количество комментариев в этом обсуждении
            int total = int.Parse(json["response"]["count"].ToString());

            //Сдвиг на сколько комментариев уже передвинулись в этом обсуждении
            int offset = 0;
            if (json["response"]["real_offset"] == null)
                offset = 0;
            else
                offset = int.Parse(json["response"]["real_offset"].ToString());

            //Вывод прогресса
            callback.Invoke((((double)offset / (double)total) * 100d));
            Application.DoEvents();

            //Рекурсивно добавляем остальные комментарии
            if (total - offset > res.CountComents)
                res.Add(GetCommentsFrom(t, res.Comments[res.CountComents - 1], callback));

            //Если был задан стартовый элемент, то надо удалить, а то он два раза добавляется
            //да, ненаучно, но что поделать)
            if (last_comm != null)
                res.RemoveCommentAt(0);

            return res;
        }

        /// <summary>
        /// Получение access_token для сессии
        /// </summary>
        /// <returns></returns>
        private string GetToken(ulong applicationID)
        {
            //TODO: Реализовать получение токена как в документации. Сейчас токен прописан в коде - неправильный подход
            return "6d9e2282cf9749603c8f6b4b010f300b0e7ef9a20faff87f592db94d8b8c25c3188ccac5fdbe32cdba9f4";
        }
    }
}
