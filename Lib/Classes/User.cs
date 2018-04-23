﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    /// <summary>
    /// структура информации о пользователе
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// проверка пользователей на идентичность. Для работы метода List<>.Contains()
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is User)
                return this.UserID == (obj as User).UserID;
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
