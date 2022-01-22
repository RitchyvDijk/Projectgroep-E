using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.afspraakDb
{
    public partial class afspraak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HulpverlenerModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NaamZorgverlener = table.Column<string>(type: "TEXT", nullable: true),
                    beschikbareDagen = table.Column<DateTime>(type: "TEXT", nullable: false),
                    beschikbareTijden = table.Column<string>(type: "TEXT", nullable: true),
                    clientNaam = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HulpverlenerModel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "afspraakModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    voornaam = table.Column<string>(type: "TEXT", nullable: true),
                    achternaam = table.Column<string>(type: "TEXT", nullable: true),
                    jongerDan16 = table.Column<bool>(type: "INTEGER", nullable: false),
                    geboorteDatum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BSN = table.Column<string>(type: "TEXT", nullable: true),
                    emailvanOuder = table.Column<string>(type: "TEXT", nullable: true),
                    emailvanGebruiker = table.Column<string>(type: "TEXT", nullable: true),
                    gekozenDatum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    gekozenTijd = table.Column<string>(type: "TEXT", nullable: true),
                    gekozenHulpverlenerid = table.Column<int>(type: "INTEGER", nullable: true)
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
