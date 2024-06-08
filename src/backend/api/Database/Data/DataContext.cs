using System;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Database.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Biodata> Biodatas { get; set; }
        public DbSet<SidikJari> SidikJaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=tubes3db;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;");
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for Biodata
            modelBuilder.Entity<Biodata>()
                .HasKey(b => b.NIK);

            // Configure SidikJari as keyless
            modelBuilder.Entity<SidikJari>()
                .HasNoKey();
        }
    }
}
