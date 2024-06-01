using System;
using api.Data;
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

        public SidikJari? GetSidikJariByBerkasCitra(string berkasCitra, int algorithm = 0)
        {
            var sidikJaris = _context.SidikJaries.ToList();

            foreach (var sidikJari in sidikJaris)
            {
                bool isMatch = false;

                if (algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(sidikJari.BerkasCitra, berkasCitra);
                }
                else if (algorithm == 1)
                {
                    isMatch = KMP.Search(sidikJari.BerkasCitra, berkasCitra);
                }

                if (isMatch)
                {
                    return sidikJari;
                }
                else
                {
                    var similarityHandler = new SimilarityNormalHandler(berkasCitra, sidikJari.BerkasCitra);
                    if (similarityHandler.GetPercentageOfSimilarityNormal() >= 0.80)
                    {
                        return sidikJari;
                    }
                }
            }

            return null;
        }
    }
}

