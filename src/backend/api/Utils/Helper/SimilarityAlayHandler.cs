using api.Utils.Algorithm;
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
            this.NormalizeBahasa();
        }

        private void NormalizeBahasa()
        {
            this.bahasaAlay = Regex.Replace(this.bahasaAlay, @"[^\w\d]", "");
            this.bahasaNormal = Regex.Replace(this.bahasaNormal, @"[^\w\d]", "");
            this.bahasaAlay = this.bahasaAlay.ToLower();
            this.bahasaNormal = this.bahasaNormal.ToLower();
        }

        private int GetLengthBahasaAlay()
        {
            return this.bahasaAlay==null ? 0 : this.bahasaAlay.Length;
        }

        public int GetSimilarityBahasaAlay()
        {
            return LCS.ComputeSimilarityAlay(this.bahasaAlay, this.bahasaNormal);
        }

        public double GetPercentageOfSimilarityBahasaAlay()
        {
            return this.GetSimilarityBahasaAlay() / this.GetLengthBahasaAlay();
        }
    }
}
