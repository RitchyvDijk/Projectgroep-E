using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.GebruikerDb
{
    public partial class AfspraakToegevoegt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "afspraakModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    naamOuder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailvanOuder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gekozenDatumTijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gekozenTijd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gekozenHulpverlenerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afspraakModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_afspraakModel_AspNetUsers_clientId",
                        column: x => x.clientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_afspraakModel_AspNetUsers_gekozenHulpverlenerId",
                        column: x => x.gekozenHulpverlenerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_afspraakModel_clientId",
                table: "afspraakModel",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_afspraakModel_gekozenHulpverlenerId",
                table: "afspraakModel",
                column: "gekozenHulpverlenerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "afspraakModel");
        }
    }
}
