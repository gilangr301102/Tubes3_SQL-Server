using Microsoft.EntityFrameworkCore.Migrations;
using api.Utils.Helper;
using static api.Utils.Helper.Enum;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
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
                    jenis_kelamin = table.Column<KelaminEnum>(type: "nvarchar(max)", nullable: false),
                    golongan_darah = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alamat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status_perkawinan = table.Column<status_perkawinanEnum>(type: "nvarchar(max)", nullable: false),
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

            // Encrypting data before insertion
            migrationBuilder.InsertData(
                table: "biodata",
                columns: new[] { "NIK", "agama", "alamat", "golongan_darah", "jenis_kelamin", "kewarganegaraan", "nama", "pekerjaan", "status_perkawinan", "tanggal_lahir", "tempat_lahir" },
                values: new object[,]
                {
                    { AesEncryption.EncryptString("1352123456"), AesEncryption.EncryptString("Religion A"), AesEncryption.EncryptString("Address A"), AesEncryption.EncryptString("O"), AesEncryption.EncryptString("LakiLaki"), AesEncryption.EncryptString("Country A"), AesEncryption.EncryptString("John Doe"), AesEncryption.EncryptString("Job A"), AesEncryption.EncryptString("BelumMenikah"), AesEncryption.EncryptString("1990-01-01"), AesEncryption.EncryptString("City A") },
                    { AesEncryption.EncryptString("1352123457"), AesEncryption.EncryptString("Religion B"), AesEncryption.EncryptString("Address B"), AesEncryption.EncryptString("A"), AesEncryption.EncryptString("Perempuan"), AesEncryption.EncryptString("Country B"), AesEncryption.EncryptString("Jane Smith"), AesEncryption.EncryptString("Job B"), AesEncryption.EncryptString("Menikah"), AesEncryption.EncryptString("1992-02-02"), AesEncryption.EncryptString("City B") }
                });
                // values: new object[,]
                // {
                //     { "1352123456", "Religion A", "Address A", "O", 0, "Country A", "John Doe", "Job A", 0, "1990-01-01", "City A" },
                //     { "1352123457", "Religion B", "Address B", "A", 1, "Country B", "Jane Smith", "Job B", 1, "1992-02-02", "City B" }
                // });

            migrationBuilder.InsertData(
                table: "sidik_jari",
                columns: new[] { "berkas_citra", "nama" },
                values: new object[,]
                {
                    { AesEncryption.EncryptString("FingerprintImage1"), AesEncryption.EncryptString("John Doe") },
                    { AesEncryption.EncryptString("FingerprintImage2"), AesEncryption.EncryptString("Jane Smith") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "biodata");

            migrationBuilder.DropTable(
                name: "sidik_jari");
        }
    }
}
