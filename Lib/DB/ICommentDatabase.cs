using System.Collections.Generic;
using turizm.Lib.Classes;

namespace turizm.Lib.DB
{
    public interface ICommentDatabase
    {
        /// <summary>
        /// общее количество комментариев в БД
        /// </summary>
        long TotalComments { get; }
        /// <summary>
        /// общее количество пользователей в БД
        /// </summary>
        long TotalUsers { get; }

        /// <summary>
        /// поиск комментариев в БД
        /// </summary>
        /// <param name="find">слова, которые надо найти</param>
        /// <param name="exclude">слова, которые не должны попадаться в результате</param>
        /// <returns>список найденных комментариев</returns>
        List<Comment> FindComments(List<string> find, List<string> exclude);
        /// <summary>
        /// получить информацию о пользователе по заданному ID
        /// </summary>
        /// <param name="userID">ID пользователя</param>
        User GetUser(long userID);
        /// <summary>
        /// загрузить список рекламных слов
        /// </summary>
        /// <param name="advertKeywordsFileName">адрес файла с рекламными словами</param>
        void LoadAdvertKw(string advertKeywordsFileName);
        /// <summary>
        /// преобразование текстовых ссылок в массив объектов Topic
        /// </summary>
        /// <param name="links">ссылки на обсуждения</param>
        List<Topic> ParseTopicLinks(List<string> links);
    }
}