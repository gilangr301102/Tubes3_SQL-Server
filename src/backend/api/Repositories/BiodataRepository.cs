using System.Linq;
using api.Models; // Assuming Biodata model is in this namespace
// Add any additional using directives if needed to resolve assembly references

namespace api.Repositories
{
    public class BiodataRepository
    {
        private readonly Context _context; // Ensure Context is properly referenced

        public BiodataRepository(Context context)
        {
            _context = context;
        }

        public Biodata GetBiodataByName(string name)
        {
            // Assuming you have a DbSet<Biodata> named Biodatas in your DbContext
            // This assumes the name is unique; adjust accordingly if it's not
            return _context.Biodatas.FirstOrDefault(b => b.Name == name);
        }
    }
}
