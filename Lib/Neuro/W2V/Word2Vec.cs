using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turizm.Lib.DB;

namespace turizm.Lib.Neuro.W2V
{
    internal class Word2Vec
    {
        private CommentDatabase db;
        private Engine eng;

        /// <summary>
        ///  
        /// </summary>
        /// <param name="db"></param>
        public Word2Vec(CommentDatabase db)
        {
            this.db = db;
            eng = new Engine();
        }

        /// <summary>
        /// обновить вектора слов, на основании комментариев из БД
        /// </summary>
        public void Train()
        {
            db.ExportCommentsText("corpus.txt");
            eng.Train("corpus.txt", "model.bin");
        }

        /// <summary>
        /// возвращает вектор для заданного слова 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public float[] GetVector(string word)
        {
            Model mod = new Model();
            mod.LoadVectors("model.bin");
            return mod.WordVector(word);
        }
    }
}
