using System;
using api.Database.Data;
using api.Interfaces;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Helper;

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
            var sidikJaris = _context.sidik_jari.ToList();

            foreach (var sidikJari in sidikJaris)
            {
                sidikJari.berkas_citra = AesEncryption.DecryptString(sidikJari.berkas_citra);
                sidikJari.nama = AesEncryption.DecryptString(sidikJari.nama);

                bool isMatch = false;

                double similarityPercentage = 0.0; // Initialize similarity percentage

                if (algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(sidikJari.berkas_citra, berkasCitra);
                }
                else if (algorithm == 1)
                {
                    isMatch = KMP.Search(sidikJari.berkas_citra, berkasCitra);
                }

                if (isMatch)
                {
                    similarityPercentage = 1.0; // Set similarity to 100% if exact match
                }
                else
                {
                    var similarityHandler = new SimilarityNormalHandler(berkasCitra, sidikJari.berkas_citra);
                    similarityPercentage = similarityHandler.GetPercentageOfSimilarityNormal();
                }

                // If similarityPercentage is above a certain threshold, return the sidikJari
                if (similarityPercentage >= 0.80)
                {
                    similarityPercentage *= 100;
                    return new SidikJariResponse
                    {
                        Id = sidikJari.Id,
                        berkas_citra = sidikJari.berkas_citra,
                        nama = sidikJari.nama,
                        similarity = similarityPercentage.ToString("F2") + "%" // Set similarity percentage
                    };
                }
            }

            return null;
        }
    }
}

