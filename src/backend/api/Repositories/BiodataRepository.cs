using System.Linq;
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

        public ICollection<Biodata> GetBiodataByName(string name)
        {
            var biodatas = _context.Biodatas.ToList();
            var result = new List<Biodata>();

            foreach (var biodata in biodatas)
            {
                string normalName = ConverterAlayToNormal.KonversiAlayKeNormalLogic(name, biodata.Nama);
                if (BoyerMoore.Search(biodata.Nama, normalName))
                {
                    result.Add(biodata);
                }
                else
                {
                    var similarityHandler = new SimilarityAlayHandler(name, biodata.Nama);
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
