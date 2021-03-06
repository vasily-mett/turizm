﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace turizm.Lib.Classes
{
    /// <summary>
    /// Структура рекламного слова в таблице tb_advert
    /// </summary>
    public class AdvWord
    {
        private string word;
        /// <summary>
        /// Слово
        /// </summary>
        public string Word { get { return word; } set { word = value; hash = GetHash(value); } }

        /// <summary>
        /// Возвращает хэш-сумму строки
        /// </summary>
        /// <param name="value">строка</param>
        /// <returns></returns>
        private string GetHash(string value)
        {
            byte[] originalBytes;
            byte[] encodedBytes;
            MD5 md5;   //Контрольная сумма по алгоритму MD5 (чтобы не добавлялись одинаковые слова)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(value);
            encodedBytes = md5.ComputeHash(originalBytes);
            return System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(encodedBytes), "-", "").ToLower();
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        private string hash;

        /// <summary>
        /// Хэш-сумма слова
        /// </summary>
        public string Hash { get { return hash; } }

    }
}
