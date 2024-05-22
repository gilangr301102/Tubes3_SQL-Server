using api.Models; // Assuming Biodata model is in this namespace

namespace api.Repositories
{
    public class BiodataRepository
    {
        private readonly YourDbContext _context; // Inject your DbContext here

        public BiodataRepository(YourDbContext context)
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

