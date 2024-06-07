using System.Linq;
using api.Models;
using static api.Utils.Helper.Enum;

namespace api.Database.Data
{
    public static class DbSeeder
    {
        public static void Seed(DataContext context)
        {
            // Check if the database has been seeded
            if (!context.Biodatas.Any())
            {
                context.Biodatas.AddRange(
                    new Biodata
                    {
                        Nama = "John Doe",
                        TempatLahir = "City A",
                        TanggalLahir = "1990-01-01",
                        JenisKelamin = KelaminEnum.LakiLaki,
                        GolonganDarah = "O",
                        Alamat = "Address A",
                        Agama = "Religion A",
                        StatusPerkawinan = StatusPerkawinanEnum.BelumMenikah,
                        Pekerjaan = "Job A",
                        Kewarganegaraan = "Country A"
                    },
                    new Biodata
                    {
                        Nama = "Jane Smith",
                        TempatLahir = "City B",
                        TanggalLahir = "1992-02-02",
                        JenisKelamin = KelaminEnum.Perempuan,
                        GolonganDarah = "A",
                        Alamat = "Address B",
                        Agama = "Religion B",
                        StatusPerkawinan = StatusPerkawinanEnum.Menikah,
                        Pekerjaan = "Job B",
                        Kewarganegaraan = "Country B"
                    }
                );

                context.SaveChanges();
            }

            if (!context.SidikJaries.Any())
            {
                context.SidikJaries.AddRange(
                    new SidikJari
                    {
                        BerkasCitra = "FingerprintImage1",
                        Nama = "John Doe"
                    },
                    new SidikJari
                    {
                        BerkasCitra = "FingerprintImage2",
                        Nama = "Jane Smith"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
