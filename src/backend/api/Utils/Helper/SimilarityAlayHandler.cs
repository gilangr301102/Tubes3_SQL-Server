using api.Utils.Algorithm;

namespace api.Utils.Helper
{
    public class SimilarityAlayHandler
    {
        private readonly string bahasaAlay;
        private readonly string bahasaNormal;

        public SimilarityAlayHandler(string bahasaAlay, string bahasaNormal)
        {
            this.bahasaAlay = bahasaAlay;
            this.bahasaNormal = bahasaNormal;
        }

        private int GetLengthBahasaNormal()
        {
            return this.bahasaNormal.Length;
        }

        public int GetSimilarityBahasaAlay()
        {
            return LCS.ComputeSimilarityAlay(bahasaAlay, bahasaNormal);
        }

        public float GetPercentageOfSimilarityBahasaAlay()
        {
            return this.GetSimilarityBahasaAlay() / this.GetLengthBahasaNormal();
        }
    }
}
