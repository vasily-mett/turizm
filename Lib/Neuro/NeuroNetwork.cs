using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using turizm.Lib.DB;
using turizm.Lib.Neuro.W2V;

namespace turizm.Lib.Neuro
{
    public class NeuroNetwork
    {
        private CommentDatabase db;
        private Word2Vec w2v;

        public NeuroNetwork(CommentDatabase db)
        {
            this.db = db;
            w2v = new Word2Vec(db);
            w2v.Train();
        }

        public void test()
        {
           db.ExportCommentsText("corpus.txt");

            Engine eng = new Engine();
            eng.Train("corpus.txt", "model.bin");

            Model mod = new Model();
            mod.LoadVectors("model.bin");
            float[] vec1 = mod.WordVector("турция");
            float[] vec2 = mod.WordVector("москва");
            float[] vec3 = mod.WordVector("не");
            float[] vec4 = mod.WordVector("анализ");

            var v1 = mod.NearestWords("турция", 10);
            var v2 = mod.NearestWords("москва", 10);
            var v3 = mod.NearestWords("не", 10);
            var v4 = mod.NearestWords("анализ", 10);

            
            

        }
    }
}
