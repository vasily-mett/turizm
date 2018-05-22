using System.Windows.Forms;
using turizm.Lib.DB;

namespace turizm.Lib.VK
{
    public interface IVK
    {
        /// <summary>
        /// обновление базы данных комментариев 
        /// </summary>
        /// <param name="database">база данных</param>
        /// <param name="options">настройки</param>
        /// <param name="callback">обратная связь</param>
        void UpdateDB(CommentDatabase database, Options options, Label labelProgress);
    }
}