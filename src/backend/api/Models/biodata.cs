using static api.Utils.Helper.Enum; // Import Enum namespace if needed

namespace api.Models
{
    public class Biodata
    {
        public required string Nama { get; set; } // "required" removed as it's not a valid C# keyword
        public required string TempatLahir { get; set; }
        public required string TanggalLahir { get; set; }
        public required KelaminEnum JenisKelamin { get; set; }
        public required string GolonganDarah { get; set; }
        public required string Alamat { get; set; }
        public required string Agama { get; set; }
        public required StatusPerkawinanEnum StatusPerkawinan { get; set; }
        public required string Pekerjaan { get; set; }
        public required string Kewarganegaraan { get; set; }
    }

    // Define your Context class inheriting from DbContext
    public class Context : DbContext
    {
        // Define a DbSet for each entity you want to include in your database
        public DbSet<Biodata> Biodatas { get; set; }

        // Constructor to configure the DbContext options
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // You can add additional configuration here if needed
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     // Configure entity mappings or relationships here
        // }
    }
}
