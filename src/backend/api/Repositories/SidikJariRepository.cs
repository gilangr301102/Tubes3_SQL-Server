using System;
using api.Database.Data;
using api.Interfaces;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;

namespace api.Repositories
{
    public class SidikJariRepository : ISidikJariRepository
	{
        private readonly DataContext _context; // Ensure Context is properly referenced

        public SidikJariRepository(DataContext context)
        {
            _context = context;
        }

        public SidikJariResponse? GetSidikJariByberkas_citra(string berkasCitra, int algorithm = 0)
        {
            var sidikJaris = _context.sidik_jari.ToArray();
            var szSidikJaris = sidikJaris.Length;
            var tempIndex = -1;
            var maxSimilarityPercentage = 0.0;
            string[] berkasCitraEl = ImageConverter.Get30PixelAscii(berkasCitra);
            int szBerkasCitra = berkasCitraEl.Length;
            for (int j = 0; j < szSidikJaris; j++)
            {
                double similarityCount = 0;
                int countBerkasCitraLength = 0;
                for(int i = 0; i<szBerkasCitra; i++){
                    countBerkasCitraLength += berkasCitraEl[i].Length;
                    bool isMatch = false;
                    if (algorithm == 0)
                    {
                        isMatch = BoyerMoore.Search(sidikJaris[j].berkas_citra, berkasCitraEl[i]);
                    }
                    else if (algorithm == 1)
                    {
                        isMatch = KMP.Search(sidikJaris[j].berkas_citra, berkasCitraEl[i]);
                    }

                    if (isMatch)
                    {
                        similarityCount += 1.0; // Set similarity to 100% if exact match
                    }
                    else
                    {
                        similarityCount += LCS.ComputeSimilarityCommon(berkasCitraEl[i], sidikJaris[j].berkas_citra) / Math.Sqrt(sidikJaris[j].berkas_citra.Length * berkasCitraEl[i].Length);
                    }
                }

                double similarityPercentage = similarityCount / szBerkasCitra;

                // If similarityPercentage is above a certain threshold, return the sidikJari
                if (similarityPercentage >= 0.70 && maxSimilarityPercentage <= similarityPercentage)
                {
                    tempIndex = j;
                    maxSimilarityPercentage = similarityPercentage;
                }
            }

            if (tempIndex == -1)
            {
                return null;
            }
            else
            {
                maxSimilarityPercentage *= 100;
                return new SidikJariResponse
                {
                    Id = sidikJaris[tempIndex].Id,
                    berkas_citra = sidikJaris[tempIndex].berkas_citra,
                    nama = sidikJaris[tempIndex].nama,
                    similarity = maxSimilarityPercentage.ToString("F2") + "%" // Set similarity percentage
                };
            }
        }
    }
}

