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
using System.Text.Json;

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

            var dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "datas.json");
            var data = LoadDataFromFile(dataFilePath);

            var imagePaths = GetAllImagePaths();

            var randomNames = GenerateRandomNames(10, data);

            var usedNIKs = new HashSet<string>();

            var random = new Random();

            var biodataEntries = new List<BiodataMigration>();
            var sidikJariEntries = new List<SidikJariMigration>();

            for (int i = 0; i < 18; i++)
            {
                Console.WriteLine($"Processing entry {i + 1}/{18}...");

                string nik;
                do
                {
                    nik = GenerateRandomNIK(random);
                } while (usedNIKs.Contains(nik));
                usedNIKs.Add(nik);

                var name = randomNames[i % 10];

                var binaryMatrix = ImageConverter.BitmapToBinaryMatrix(imagePaths[i]);
                var asciiSegments = ImageConverter.Get30PixelAscii(binaryMatrix);
                var asciiBuilder = new StringBuilder();
                foreach (var segment in asciiSegments)
                {
                    asciiBuilder.Append(segment);
                }
                var base64Segment = asciiBuilder.ToString();

                var gender = random.Next(2) == 0 ? "LakiLaki" : "Perempuan";
                var bloodType = data.bloodTypes[random.Next(data.bloodTypes.Count)];
                var maritalStatus = data.maritalStatuses[random.Next(data.maritalStatuses.Count)];
                var city = data.cityNames[random.Next(data.cityNames.Count)];

                DateTime startDate = new DateTime(1950, 1, 1);
                DateTime endDate = new DateTime(2000, 12, 31);
                TimeSpan range = endDate - startDate;
                var randomBirthDate = startDate.AddDays(random.Next(range.Days));

                var randomJob = data.jobs[random.Next(data.jobs.Count)];
                var randomNationality = data.nationalities[random.Next(data.nationalities.Count)];
                var randomReligion = data.religions[random.Next(data.religions.Count)];
                var randomAddress = data.addresses[random.Next(data.addresses.Count)];

                biodataEntries.Add(new BiodataMigration
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

                var alayName = ConvertNormalToAlay.ConvertToAlay(name);

                string berkasCitra = base64Segment;

                sidikJariEntries.Add(new SidikJariMigration
                {
                    berkas_citra = AesEncryption.EncryptString(berkasCitra),
                    nama = AesEncryption.EncryptString(alayName)
                });
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
            var imagePaths = Directory.GetFiles(datasetDirectory, "*.BMP");

            return imagePaths;
        }

        private static string GenerateRandomNIK(Random random)
        {
            return random.Next(100000, 999999).ToString("D10");
        }

        private static string[] GenerateRandomNames(int count, DataModel data)
        {
            var random = new Random();
            return Enumerable.Range(0, count)
                             .Select(_ => data.names[random.Next(data.names.Count)])
                             .ToArray();
        }

        private static DataModel LoadDataFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<DataModel>(json);
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
