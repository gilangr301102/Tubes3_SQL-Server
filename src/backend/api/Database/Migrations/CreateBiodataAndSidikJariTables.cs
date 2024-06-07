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
                    table.PrimaryKey("PK_Biodatas", x => new { x.Nama, x.TempatLahir, x.TanggalLahir });
                });

            migrationBuilder.CreateTable(
                name: "SidikJaries",
                columns: table => new
                {
                    BerkasCitra = table.Column<string>(nullable: false),
                    Nama = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SidikJaries", x => new { x.BerkasCitra, x.Nama });
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
