// using System;
// using System.Linq;
// using api.Database.Data;
// using api.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using static api.Utils.Helper.Enum;

// namespace api.Database.Seeders
// {
//     public class DbSeeder
//     {
//         private readonly ModelBuilder modelBuilder;

//         public DbSeeder(ModelBuilder modelBuilder)
//         {
//             this.modelBuilder = modelBuilder;
//         }
//         public void Seed(DataContext context, ILogger<DbSeeder> logger)
//         {
//             try
//             {
//                 // Check if the database has been seeded
//                 if (!context.Biodatas.Any())
//                 {
//                     context.Biodatas.AddRange(
//                         new Biodata
//                         {
//                             Nama = "John Doe",
//                             TempatLahir = "City A",
//                             TanggalLahir = "1990-01-01",
//                             JenisKelamin = KelaminEnum.LakiLaki,
//                             GolonganDarah = "O",
//                             Alamat = "Address A",
//                             Agama = "Religion A",
//                             StatusPerkawinan = StatusPerkawinanEnum.BelumMenikah,
//                             Pekerjaan = "Job A",
//                             Kewarganegaraan = "Country A"
//                         },
//                         new Biodata
//                         {
//                             Nama = "Jane Smith",
//                             TempatLahir = "City B",
//                             TanggalLahir = "1992-02-02",
//                             JenisKelamin = KelaminEnum.Perempuan,
//                             GolonganDarah = "A",
//                             Alamat = "Address B",
//                             Agama = "Religion B",
//                             StatusPerkawinan = StatusPerkawinanEnum.Menikah,
//                             Pekerjaan = "Job B",
//                             Kewarganegaraan = "Country B"
//                         }
//                     );

//                     context.SaveChanges();
//                 }

//                 if (!context.SidikJaries.Any())
//                 {
//                     context.SidikJaries.AddRange(
//                         new SidikJari
//                         {
//                             BerkasCitra = "FingerprintImage1",
//                             Nama = "John Doe"
//                         },
//                         new SidikJari
//                         {
//                             BerkasCitra = "FingerprintImage2",
//                             Nama = "Jane Smith"
//                         }
//                     );

//                     context.SaveChanges();
//                 }
//             }
//             // catch (Exception ex)
//             // {
//             //     logger.LogError(ex, "An error occurred while seeding the database.");
//             //     throw; // Re-throw the exception to be handled by the caller if necessary
//             // }
//         }
//     }
// }

using System;
using System.Linq;
using api.Database.Data;
using api.Models;
using Microsoft.Extensions.Logging;
using static api.Utils.Helper.Enum;

namespace api.Database.Seeders
{
    public class DbSeeder
    {
        public void Seed(DataContext context, ILogger<DbSeeder> logger)
        {
            try
            {
                // Check if the database has been seeded
                if (!context.Biodatas.Any())
                {
                    context.Biodatas.AddRange(
                        new Biodata
                        {
                            NIK = "1352123456",
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
                            NIK = "1352123457",
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
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw; // Re-throw the exception to be handled by the caller if necessary
            }
        }
    }
}
