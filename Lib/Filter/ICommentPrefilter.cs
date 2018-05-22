using System.Collections.Generic;
using turizm.Lib.Classes;

namespace turizm.Lib.Filter
{
    public interface ICommentPrefilter
    {
        /// <summary>
        /// для каждого комментария в списке записывает список рекламных масок, под которые подходит текст
        /// </summary>
        /// <param name="comments">комментарии</param>
        List<Comment> CountAdvertWords(List<Comment> comments);
    }
}