using System.Collections.Generic;
using System.Linq;
using api.Database.Data;
using api.Interfaces;
using api.Models;
using api.Utils.Algorithm;
using api.Utils.Converter;
using api.Utils.Helper;

namespace api.Repositories
{
    public class BiodataRepository : IBiodataRepository
    {
        private readonly DataContext _context;

        public BiodataRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Biodata> GetBiodataByName(string name, int algorithm = 0)
        {
            var biodatas = _context.Biodata.ToList();
            var result = new List<Biodata>();

            foreach (var biodata in biodatas)
            {
                string normalName = ConverterAlayToNormal.KonversiAlayKeNormalLogic(name, biodata.nama);
                bool isMatch = false;

                if (algorithm == 0)
                {
                    isMatch = BoyerMoore.Search(biodata.nama, normalName);
                }
                else if (algorithm == 1)
                {
                    isMatch = KMP.Search(biodata.nama, normalName);
                }

                if (isMatch)
                {
                    result.Add(biodata);
                }
                else
                {
                    var similarityHandler = new SimilarityAlayHandler(normalName, biodata.nama);
                    if (similarityHandler.GetPercentageOfSimilarityBahasaAlay() >= 0.71)
                    {
                        result.Add(biodata);
                    }
                }
            }

            return result;
        }
    }
}
