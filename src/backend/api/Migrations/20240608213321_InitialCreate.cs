using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "Biodatas",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tempat_lahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tanggal_lahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jenis_kelamin = table.Column<int>(type: "int", nullable: false),
                    golongan_darah = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alamat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status_perkawinan = table.Column<int>(type: "int", nullable: false),
                    pekerjaan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kewarganegaraan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biodatas", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "SidikJaries",
                columns: table => new
                {
                    berkas_citra = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SidikJaries", x => x.berkas_citra);
                });

            migrationBuilder.InsertData(
                table: "Biodatas",
                columns: new[] { "NIK", "agama", "alamat", "golongan_darah", "jenis_kelamin", "kewarganegaraan", "nama", "pekerjaan", "status_perkawinan", "tanggal_lahir", "tempat_lahir" },
                values: new object[,]
                {
                    { "1352123456", "Religion A", "Address A", "O", 0, "Country A", "John Doe", "Job A", 0, "1990-01-01", "City A" },
                    { "1352123457", "Religion B", "Address B", "A", 1, "Country B", "Jane Smith", "Job B", 1, "1992-02-02", "City B" }
                });

            migrationBuilder.InsertData(
                table: "SidikJaries",
                columns: new[] { "berkas_citra", "nama" },
                values: new object[,]
                {
                    { "FingerprintImage1", "John Doe" },
                    { "FingerprintImage2", "Jane Smith" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biodatas");

            migrationBuilder.DropTable(
                name: "SidikJaries");
        }
    }
}
