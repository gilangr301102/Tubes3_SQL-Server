using api.Utils.Algorithm;
using api.Utils.Converter;
using System.Text.RegularExpressions;

namespace api.Utils.Helper
{
    public class SimilarityAlayHandler
    {
        private string bahasaAlay;
        private string bahasaNormal;

        public SimilarityAlayHandler(string bahasaAlay, string bahasaNormal)
        {
            this.bahasaAlay = bahasaAlay;
            this.bahasaNormal = bahasaNormal;
        }

        public double GetGeometricMeanTwoString()
        {
            return Math.Sqrt(this.bahasaAlay.Length * this.bahasaNormal.Length);
        }


        public int GetSimilarityBahasaAlay()
        {
            return LCS.ComputeSimilarityAlay(this.bahasaAlay, this.bahasaNormal);
        }

        public double GetPercentageOfSimilarityBahasaAlay()
        {
            return this.GetSimilarityBahasaAlay() / this.GetGeometricMeanTwoString();
        }
    }
}
