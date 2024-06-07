using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Database.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Biodata> Biodatas { get; set; }
        public DbSet<SidikJari> SidikJaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=tubes3db;Trusted_Connection=True;");
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary keys
            modelBuilder.Entity<Biodata>()
                .HasKey(b => new { b.Nama, b.TempatLahir, b.TanggalLahir });

            modelBuilder.Entity<SidikJari>()
                .HasKey(s => new { s.BerkasCitra, s.Nama });
        }
    }
}
