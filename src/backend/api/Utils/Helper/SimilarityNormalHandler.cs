using System;
using api.Utils.Algorithm;

namespace api.Utils.Helper
{
    public class SimilarityNormalHandler
    {
        private readonly string str1;
        private readonly string str2;

        public SimilarityNormalHandler(string str1, string str2)
        {
            this.str1 = str1;
            this.str2 = str2;
        }

        private int GetLengthStr2()
        {
            return this.str2.Length;
        }

        public int GetSimilarityBahasaNormal()
        {
            return LCS.ComputeSimilarityCommon(str1, str2);
        }

        public float GetPercentageOfSimilarityNormal()
        {
            return this.GetSimilarityBahasaNormal() / this.GetLengthStr2();
        }
    }
}
