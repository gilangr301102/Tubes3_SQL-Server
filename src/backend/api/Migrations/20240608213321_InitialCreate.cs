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

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    berkas_citra = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sidik_jari", x => x.berkas_citra);
                });

            var imagePaths = GetAllImagePaths();
            Console.WriteLine("Debug image paths length: ");
            Console.WriteLine(imagePaths.Length);
            var randomNames = GenerateRandomNames(10);

            var usedNIKs = new HashSet<string>();

            var random = new Random();

            for (int i = 0; i < imagePaths.Length; i++)
            {
                var biodataEntries = new List<object>();
                var sidikJariEntries = new List<object>();
                Console.WriteLine($"Processing entry {i + 1}/{imagePaths.Length}...");

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

                biodataEntries.Add(new
                {
                    NIK = AesEncryption.EncryptString(nik),
                    agama = AesEncryption.EncryptString("Religion " + (char)('A' + i)),
                    alamat = AesEncryption.EncryptString("Address " + (char)('A' + i)),
                    golongan_darah = AesEncryption.EncryptString("O"),
                    jenis_kelamin = AesEncryption.EncryptString("LakiLaki"),
                    kewarganegaraan = AesEncryption.EncryptString("Country " + (char)('A' + i)),
                    nama = AesEncryption.EncryptString(name),
                    pekerjaan = AesEncryption.EncryptString("Job " + (char)('A' + i)),
                    status_perkawinan = AesEncryption.EncryptString("BelumMenikah"),
                    tanggal_lahir = AesEncryption.EncryptString("1990-01-01"),
                    tempat_lahir = AesEncryption.EncryptString("City " + (char)('A' + i))
                });

                var alayName = ConvertToAlay(name);

                string berkasCitra = base64Segment;

                sidikJariEntries.Add(new
                {
                    berkas_citra = AesEncryption.EncryptString(berkasCitra),
                    nama = AesEncryption.EncryptString(alayName)
                });

                migrationBuilder.InsertData(
                    table: "biodata",
                    columns: new[] { "NIK", "agama", "alamat", "golongan_darah", "jenis_kelamin", "kewarganegaraan", "nama", "pekerjaan", "status_perkawinan", "tanggal_lahir", "tempat_lahir" },
                    values: biodataEntries.ToArray());

                migrationBuilder.InsertData(
                    table: "sidik_jari",
                    columns: new[] { "berkas_citra", "nama" },
                    values: sidikJariEntries.ToArray());

                Console.WriteLine($"Completed entry {i + 1}/{imagePaths.Length}");
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
