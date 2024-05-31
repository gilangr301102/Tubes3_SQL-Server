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

        public int GetLengthStr1()
        {
            return this.str1==null ? 0 : this.str1.Length;
        }

        private int GetLengthStr2()
        {
            return this.str2 == null ? 0 : this.str2.Length;
        }

        public int GetSimilarityBahasaNormal()
        {
            return LCS.ComputeSimilarityCommon(str1, str2);
        }

        public double GetGeometricMeanTwoString()
        {
            return Math.Sqrt(this.GetLengthStr1() * this.GetLengthStr2());
        }

        public double GetPercentageOfSimilarityNormal()
        {
            return this.GetSimilarityBahasaNormal() / this.GetGeometricMeanTwoString();
        }
    }
}
