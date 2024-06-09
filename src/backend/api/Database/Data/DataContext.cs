using System;
using api.Models;
using Microsoft.EntityFrameworkCore;
using static api.Utils.Helper.Enum;

namespace api.Database.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Biodata> Biodatas { get; set; }
        public DbSet<SidikJari> SidikJaries { get; set; }

        // public IConfiguration Configuration { get; }

        // public DataContext(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        // }

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
                .HasKey(b => b.berkas_citra);

            modelBuilder.Entity<Biodata>().HasData(
                new Biodata
                {
                    NIK = "1352123456",
                    nama = "John Doe",
                    tempat_lahir = "City A",
                    tanggal_lahir = "1990-01-01",
                    jenis_kelamin = KelaminEnum.LakiLaki,
                    golongan_darah = "O",
                    alamat = "Address A",
                    agama = "Religion A",
                    status_perkawinan = status_perkawinanEnum.BelumMenikah,
                    pekerjaan = "Job A",
                    kewarganegaraan = "Country A"
                },
                new Biodata
                {
                    NIK = "1352123457",
                    nama = "Jane Smith",
                    tempat_lahir = "City B",
                    tanggal_lahir = "1992-02-02",
                    jenis_kelamin = KelaminEnum.Perempuan,
                    golongan_darah = "A",
                    alamat = "Address B",
                    agama = "Religion B",
                    status_perkawinan = status_perkawinanEnum.Menikah,
                    pekerjaan = "Job B",
                    kewarganegaraan = "Country B"
                }
            );


            modelBuilder.Entity<SidikJari>().HasData(
                new SidikJari
                {
                    berkas_citra = "FingerprintImage1",
                    nama = "John Doe"
                },
                new SidikJari
                {
                    berkas_citra = "FingerprintImage2",
                    nama = "Jane Smith"
                }
            );
        }
    }
}
