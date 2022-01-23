using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.afspraakDb
{
    public partial class afspraakCorrectie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_afspraakModel_HulpverlenerModel_gekozenHulpverlenerid",
                table: "afspraakModel");

            migrationBuilder.DropTable(
                name: "HulpverlenerModel");

            migrationBuilder.DropIndex(
                name: "IX_afspraakModel_gekozenHulpverlenerid",
                table: "afspraakModel");

            migrationBuilder.DropColumn(
                name: "gekozenHulpverlenerid",
                table: "afspraakModel");

            migrationBuilder.AddColumn<string>(
                name: "gekozenHulpverlener",
                table: "afspraakModel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "naamOuder",
                table: "afspraakModel",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gekozenHulpverlener",
                table: "afspraakModel");

            migrationBuilder.DropColumn(
                name: "naamOuder",
                table: "afspraakModel");

            migrationBuilder.AddColumn<int>(
                name: "gekozenHulpverlenerid",
                table: "afspraakModel",
                type: "INTEGER",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_afspraakModel_gekozenHulpverlenerid",
                table: "afspraakModel",
                column: "gekozenHulpverlenerid");

            migrationBuilder.AddForeignKey(
                name: "FK_afspraakModel_HulpverlenerModel_gekozenHulpverlenerid",
                table: "afspraakModel",
                column: "gekozenHulpverlenerid",
                principalTable: "HulpverlenerModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
