using Microsoft.EntityFrameworkCore.Migrations;
using api.Utils.Helper;
using api.Utils.Converter;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using api.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using api.Models;

#nullable disable

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "biodata",
            columns: table => new
            {
                NIK = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                tempat_lahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                tanggal_lahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                jenis_kelamin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                golongan_darah = table.Column<string>(type: "nvarchar(max)", nullable: false),
                alamat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                agama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                status_perkawinan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                pekerjaan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                kewarganegaraan = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_biodata", x => x.NIK);
            });

            migrationBuilder.CreateTable(
            name: "sidik_jari",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                berkas_citra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                nama = table.Column<string>(type: "nvarchar(200)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_sidik_jari", x => x.Id);
            });

            var imagePaths = GetAllImagePaths();
            Console.WriteLine("Debug image paths length: ");
            Console.WriteLine(11);
            var randomNames = GenerateRandomNames(10);

            var usedNIKs = new HashSet<string>();

            var random = new Random();

            var biodataEntries = new List<BiodataRequest>();
            var sidikJariEntries = new List<SidikJariRequest>();

            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine($"Processing entry {i + 1}/{11}...");

                string nik;
                do
                {
                    nik = GenerateRandomNIK(random);
                } while (usedNIKs.Contains(nik));
                usedNIKs.Add(nik);

                var name = randomNames[i % 10];

                var binaryMatrix = ImageConverter.BitmapToBinaryMatrix(imagePaths[i]);
                var asciiSegments = ImageConverter.Get30PixelAscii(binaryMatrix);
                var base64Segment = ImageConverter.EncodeAsciiToBase64(asciiSegments[0]);
                var gender = random.Next(2) == 0 ? "LakiLaki" : "Perempuan";
                var bloodTypes = new string[] { "O", "A", "B", "AB" };
                var bloodType = bloodTypes[random.Next(bloodTypes.Length)];
                var maritalStatuses = new string[] { "Menikah", "BelumMenikah", "Cerai" };
                var maritalStatus = maritalStatuses[random.Next(maritalStatuses.Length)];
                var cityNames = new string[] { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose", "Austin", "Jacksonville", "San Francisco", "Columbus", "Fort Worth", "Indianapolis", "Charlotte", "Seattle", "Denver", "Washington", "Boston", "El Paso", "Detroit", "Nashville", "Portland", "Memphis", "Oklahoma City", "Las Vegas", "Louisville", "Baltimore", "Milwaukee", "Albuquerque", "Tucson", "Fresno", "Sacramento", "Mesa", "Atlanta", "Kansas City", "Colorado Springs", "Miami", "Raleigh", "Omaha", "Long Beach", "Virginia Beach", "Oakland", "Minneapolis", "Tulsa", "Arlington", "Tampa" };
                var city = cityNames[random.Next(cityNames.Length)];
                DateTime startDate = new DateTime(1950, 1, 1);
                DateTime endDate = new DateTime(2000, 12, 31);
                TimeSpan range = endDate - startDate;
                var randomBirthDate = startDate.AddDays(random.Next(range.Days));
                string[] jobs = new string[]
                {
            "Software Developer",
            "Teacher",
            "Nurse",
            "Doctor",
            "Engineer",
            "Accountant",
            "Chef",
            "Writer",
            "Artist",
            "Electrician"
                };
                var randomJob = jobs[random.Next(jobs.Length)];

                string[] nationalities = new string[]
                {
            "Indonesia",
            "United States",
            "China",
            "India",
            "Brazil",
            "Pakistan",
            "Nigeria",
            "Bangladesh",
            "Russia",
            "Mexico"
                };

                var randomNationality = nationalities[random.Next(nationalities.Length)];

                string[] religions = new string[]
                {
            "Islam",
            "Christianity",
            "Hinduism",
            "Buddhism",
            "Sikhism",
            "Judaism",
            "Bahá'í Faith",
            "Jainism",
            "Shinto",
            "Taoism"
                };

                var randomReligion = religions[random.Next(religions.Length)];

                string[] addresses = new string[]
                {
            "123 Main Street",
            "456 Elm Avenue",
            "789 Oak Lane",
            "101 Pine Road",
            "202 Maple Court",
            "303 Cedar Drive",
            "404 Walnut Boulevard",
            "505 Spruce Way",
            "606 Birch Street",
            "707 Sycamore Lane"
                };

                var randomAddress = addresses[random.Next(addresses.Length)];

                biodataEntries.Add(new BiodataRequest
                {
                    NIK = AesEncryption.EncryptString(nik),
                    agama = AesEncryption.EncryptString(randomReligion),
                    alamat = AesEncryption.EncryptString(randomAddress),
                    golongan_darah = AesEncryption.EncryptString(bloodType),
                    jenis_kelamin = AesEncryption.EncryptString(gender),
                    kewarganegaraan = AesEncryption.EncryptString(randomNationality),
                    nama = AesEncryption.EncryptString(name),
                    pekerjaan = AesEncryption.EncryptString(randomJob),
                    status_perkawinan = AesEncryption.EncryptString(maritalStatus),
                    tanggal_lahir = AesEncryption.EncryptString(randomBirthDate.ToString("yyyy-MM-dd")),
                    tempat_lahir = AesEncryption.EncryptString(city)
                });

                var alayName = ConvertToAlay(name);

                string berkasCitra = base64Segment;

                var truncatedBerkasCitra = berkasCitra.Substring(0, Math.Min(berkasCitra.Length, 450));
                sidikJariEntries.Add(new SidikJariRequest
                {
                    berkas_citra = AesEncryption.EncryptString(truncatedBerkasCitra),
                    nama = AesEncryption.EncryptString(alayName)
                });

                Console.WriteLine($"Completed entry {i + 1}/{11}");
                Console.WriteLine("Biodata entry:");
                Console.WriteLine($"NIK: {biodataEntries.Last().NIK}");
                Console.WriteLine($"Nama: {biodataEntries.Last().nama}");
                Console.WriteLine($"Tempat Lahir: {biodataEntries.Last().tempat_lahir}");
                Console.WriteLine($"Tanggal Lahir: {biodataEntries.Last().tanggal_lahir}");
                Console.WriteLine($"Jenis Kelamin: {biodataEntries.Last().jenis_kelamin}");
                Console.WriteLine($"Golongan Darah: {biodataEntries.Last().golongan_darah}");
                Console.WriteLine($"Alamat: {biodataEntries.Last().alamat}");
                Console.WriteLine($"Agama: {biodataEntries.Last().agama}");
                Console.WriteLine($"Status Perkawinan: {biodataEntries.Last().status_perkawinan}");
                Console.WriteLine($"Pekerjaan: {biodataEntries.Last().pekerjaan}");
                Console.WriteLine($"Kewarganegaraan: {biodataEntries.Last().kewarganegaraan}");
                Console.WriteLine("Sidik Jari entry:");
                Console.WriteLine($"Berkas Citra: {sidikJariEntries.Last().berkas_citra}");
                Console.WriteLine($"Nama: {sidikJariEntries.Last().nama}");
            }


            foreach(var entry in  biodataEntries)
            {
                migrationBuilder.InsertData(
                    table: "biodata",
                    columns: new[] { "NIK", "nama", "tempat_lahir", "tanggal_lahir", "jenis_kelamin", "golongan_darah", "alamat", "agama", "status_perkawinan", "pekerjaan", "kewarganegaraan" },
                    values: new object[] { entry.NIK, entry.nama, entry.tempat_lahir, entry.tanggal_lahir, entry.jenis_kelamin, entry.golongan_darah, entry.alamat, entry.agama, entry.status_perkawinan, entry.pekerjaan, entry.kewarganegaraan });
            }

            foreach (var entry in sidikJariEntries)
            {
                migrationBuilder.InsertData(
                    table: "sidik_jari",
                    columns: new[] { "berkas_citra", "nama" },
                    values: new object[] { entry.berkas_citra, entry.nama });
            }
        }

        private static string[] GetAllImagePaths()
        {
            var datasetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Dataset");
            return Directory.GetFiles(datasetDirectory, "*.BMP");
        }

        private static string GenerateRandomNIK(Random random)
        {
            return random.Next(100000, 999999).ToString("D10");
        }

        private static string[] GenerateRandomNames(int count)
        {
            var names = new string[]
            {
                "John Doe", "Jane Smith", "Alice Johnson", "Robert Brown", "Michael Davis",
                "Emily Wilson", "Daniel Martinez", "Sophia Anderson", "James Taylor", "Grace Thomas"
            };
            var random = new Random();
            return Enumerable.Range(0, count)
                             .Select(_ => names[random.Next(names.Length)])
                             .ToArray();
        }

        private static string ConvertToAlay(string name)
        {
            var alayWords = new Dictionary<char, char>
            {
                { 'a', '4' }, { 'b', '8' }, { 'e', '3' }, { 'i', '1' }, { 'o', '0' },
                { 's', '5' }, { 't', '7' }, { 'z', '2' }
            };

            var random = new Random();
            var sb = new StringBuilder();

            foreach (var c in name.ToLower())
            {
                if (alayWords.ContainsKey(c))
                {
                    sb.Append(alayWords[c]);
                }
                else if (c == 'g')
                {
                    sb.Append(random.Next(2) == 0 ? '6' : '9');  // Randomly choose between '6' and '9'
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "biodata");

            migrationBuilder.DropTable(
                name: "sidik_jari");
        }
    }
}

