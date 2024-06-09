using Microsoft.EntityFrameworkCore;
using api.Utils.Helper;
using System;
using System.Linq;
using api.Models;

namespace api.Database.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Biodata> Biodata { get; set; }
        public DbSet<SidikJari> SidikJari { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for Biodata
            modelBuilder.Entity<Biodata>()
                .HasKey(b => b.NIK);

            // Configure SidikJari as keyless
            modelBuilder.Entity<SidikJari>()
                .HasKey(b => b.berkas_citra);
        }
    }
}
