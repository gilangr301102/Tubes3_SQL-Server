using System.Linq;
using api.Data;
using api.Models; // Assuming Biodata model is in this namespace
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
            // Assuming you have a DbSet<Biodata> named Biodatas in your DbContext
            // This assumes the name is unique; adjust accordingly if it's not
            return _context.Biodatas.Where(b => b.Nama == name).ToList();
        }
    }
}
