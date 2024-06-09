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

        public DbSet<BiodataMigration> biodata { get; set; }

        public DbSet<SidikJariMigration> sidik_jari { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for BiodataResponse
            modelBuilder.Entity<BiodataMigration>()
                .HasKey(b => b.NIK);

            // Configure SidikJariResponse as keyless
            modelBuilder.Entity<SidikJariMigration>()
                .HasKey(b => b.Id);
        }
    }
}
