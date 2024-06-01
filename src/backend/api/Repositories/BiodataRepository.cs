﻿using System.Linq;
using System.Security.Cryptography;
using api.Data;
using api.Models; // Assuming Biodata model is in this namespace
using api.Utils.Algorithm;
using api.Utils.Converter;
using api.Utils.Helper;
// Add any additional using directives if needed to resolve assembly references

namespace api.Repositories
{
    public class BiodataRepository
    {
        private readonly DataContext _context; // Ensure Context is properly referenced

        public BiodataRepository(DataContext context)
        {
            _context = context;
        }

        public SidikJari? GetSidikJariByBerkasCitra(string berkasCitra, int algorithm = 0)
        {
            var sidikJaris = _context.SidikJaries.ToList();

            foreach(var sidikJari in sidikJaris)
            {
                bool isMatch = false;

                if(algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(sidikJari.BerkasCitra, berkasCitra);
                }
                else if(algorithm == 1)
                {
                    isMatch = KMP.Search(sidikJari.BerkasCitra, berkasCitra);
                }

                if(isMatch)
                {
                    return sidikJari;
                }
                else
                {
                    var similarityHandler = new SimilarityNormalHandler(berkasCitra, sidikJari.BerkasCitra);
                    if(similarityHandler.GetPercentageOfSimilarityNormal() >= 0.80)
                    {
                        return sidikJari;
                    }
                }
            }

            return null;
        }

        public ICollection<Biodata> GetBiodataByName(string name, int algorithm = 0)
        {
            var biodatas = _context.Biodatas.ToList();
            var result = new List<Biodata>();

            foreach (var biodata in biodatas)
            {
                string normalName = ConverterAlayToNormal.KonversiAlayKeNormalLogic(name, biodata.Nama);
                bool isMatch = false;

                if (algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(biodata.Nama, normalName);
                }
                else if (algorithm == 1)
                {
                    isMatch = KMP.Search(biodata.Nama, normalName);
                }

                if (isMatch)
                {
                    result.Add(biodata);
                }
                else
                {
                    var similarityHandler = new SimilarityAlayHandler(normalName, biodata.Nama);
                    if(similarityHandler.GetPercentageOfSimilarityBahasaAlay() >= 0.71)
                    {
                        result.Add(biodata);
                    }
                }
            }

            return result;
        }
    }
}
