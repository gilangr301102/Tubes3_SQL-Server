using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class CreateBiodataAndSidikJariTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biodatas",
                columns: table => new
                {
                    NIK = table.Column<string>(nullable: false),
                    Nama = table.Column<string>(nullable: false),
                    TempatLahir = table.Column<string>(nullable: false),
                    TanggalLahir = table.Column<string>(nullable: false),
                    JenisKelamin = table.Column<int>(nullable: false),
                    GolonganDarah = table.Column<string>(nullable: false),
                    Alamat = table.Column<string>(nullable: false),
                    Agama = table.Column<string>(nullable: false),
                    StatusPerkawinan = table.Column<int>(nullable: false),
                    Pekerjaan = table.Column<string>(nullable: false),
                    Kewarganegaraan = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biodatas", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "SidikJaries",
                columns: table => new
                {
                    BerkasCitra = table.Column<string>(nullable: false),
                    Nama = table.Column<string>(nullable: false)
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biodatas");

            migrationBuilder.DropTable(
                name: "SidikJaries");
        }
    }
}
