using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.VK
{
    public class BaseHttp
    {
        /// <summary>
        /// отправка запроса get
        /// </summary>
        /// <param name="url">url запроса</param>
        /// <returns></returns>
        protected string  GetString(string url)
        {
            try
            {
                //Выполняем запрос к универсальному коду ресурса (URI).
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36";
                request.ContentType = "application/xml";
                request.Headers[HttpRequestHeader.AcceptLanguage] = "ru - RU,ru; q = 0.8,en - US; q = 0.6,en; q = 0.4";

                //Получаем ответ от интернет-ресурса.
                WebResponse response = request.GetResponse();

                //Экземпляр класса System.IO.Stream 
                //для чтения данных из интернет-ресурса.
                Stream dataStream = response.GetResponseStream();

                //Инициализируем новый экземпляр класса 
                //System.IO.StreamReader для указанного потока.
                StreamReader sreader = new StreamReader(dataStream);

                //Считывает поток от текущего положения до конца.            
                string responsereader = sreader.ReadToEnd();

                //Закрываем поток ответа.
                response.Close();
                return responsereader;
            }
            catch (WebException we) { throw new WebException("Ошибка подключения.\r\n" + url, we); }
        }

        protected JObject GetJson(string url)
        {
            return new JObject(GetString(url));
        }
    }
}
