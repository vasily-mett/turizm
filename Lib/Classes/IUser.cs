namespace turizm.Lib.Classes
{
    public interface IUser
    {
        /// <summary>
        /// имя
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// фамилия
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// ID пользователя
        /// </summary>
        long UserID { get; set; }

        /// <summary>
        /// проверка пользователей на идентичность
        /// </summary>
        /// /// <param name="obj">пользователь</param>
        bool Equals(object obj);
        int GetHashCode();
    }
}