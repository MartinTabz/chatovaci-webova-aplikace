using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talkero.Migrations
{
    public partial class inicializace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzivatele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Heslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overen = table.Column<bool>(type: "bit", nullable: false),
                    Profilovka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zablokovan = table.Column<bool>(type: "bit", nullable: false),
                    DuvodZablokovani = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZadaOOdblokovani = table.Column<bool>(type: "bit", nullable: false),
                    JeAdmin = table.Column<bool>(type: "bit", nullable: false),
                    SouhlasiSPodminkami = table.Column<bool>(type: "bit", nullable: false),
                    ZasilatNovinky = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzivatele", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zpravy",
                columns: table => new
                {
                    ZpravaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObsahZpravy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obrazek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumOdeslani = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zpravy", x => x.ZpravaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uzivatele");

            migrationBuilder.DropTable(
                name: "Zpravy");
        }
    }
}
