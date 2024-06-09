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

        public DbSet<BiodataResponse> BiodataResponse { get; set; }
        public DbSet<SidikJariResponse> SidikJariResponse { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for BiodataResponse
            modelBuilder.Entity<BiodataRequest>()
                .HasKey(b => b.NIK);

            // Configure SidikJariResponse as keyless
            modelBuilder.Entity<SidikJariRequest>()
                .HasKey(b => b.Id);
        }
    }
}
