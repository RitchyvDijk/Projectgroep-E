using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.AfspraakDb
{
    public partial class InitialChatDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HulpverlenerModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NaamZorgverlener = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    beschikbareDagen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    beschikbareTijden = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HulpverlenerModel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "afspraakModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voornaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    achternaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jongerDan16 = table.Column<bool>(type: "bit", nullable: false),
                    geboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BSN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    naamOuder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailvanOuder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailvanGebruiker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gekozenDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gekozenTijd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gekozenHulpverlenerid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afspraakModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_afspraakModel_HulpverlenerModel_gekozenHulpverlenerid",
                        column: x => x.gekozenHulpverlenerid,
                        principalTable: "HulpverlenerModel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_afspraakModel_gekozenHulpverlenerid",
                table: "afspraakModel",
                column: "gekozenHulpverlenerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "afspraakModel");

            migrationBuilder.DropTable(
                name: "HulpverlenerModel");
        }
    }
}
