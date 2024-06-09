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

        // public override int SaveChanges()
        // {
        //     EncryptData();
        //     return base.SaveChanges();
        // }

        // private void EncryptData()
        // {
        //     var entities = ChangeTracker.Entries()
        //         .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
        //         .Select(x => x.Entity);

        //     foreach (var entity in entities)
        //     {
        //         if (entity is Biodata biodata)
        //         {
        //             biodata.agama = AesEncryption.EncryptString(biodata.agama);
        //             biodata.alamat = AesEncryption.EncryptString(biodata.alamat);
        //             biodata.golongan_darah = AesEncryption.EncryptString(biodata.golongan_darah);
        //             biodata.kewarganegaraan = AesEncryption.EncryptString(biodata.kewarganegaraan);
        //             biodata.nama = AesEncryption.EncryptString(biodata.nama);
        //             biodata.pekerjaan = AesEncryption.EncryptString(biodata.pekerjaan);
        //             biodata.tempat_lahir = AesEncryption.EncryptString(biodata.tempat_lahir);
        //             biodata.tanggal_lahir = AesEncryption.EncryptString(biodata.tanggal_lahir);
        //         }
        //         else if (entity is SidikJari sidikJari)
        //         {
        //             sidikJari.nama = AesEncryption.EncryptString(sidikJari.nama);
        //         }
        //     }
        // }
    }
}
