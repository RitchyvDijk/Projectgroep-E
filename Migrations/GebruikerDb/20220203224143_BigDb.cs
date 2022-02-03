using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.GebruikerDb
{
    public partial class BigDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_afspraakModel_AspNetUsers_gekozenHulpverlenerId",
                table: "afspraakModel");

            migrationBuilder.DropIndex(
                name: "IX_afspraakModel_gekozenHulpverlenerId",
                table: "afspraakModel");

            migrationBuilder.DropColumn(
                name: "gekozenHulpverlenerId",
                table: "afspraakModel");

            migrationBuilder.AddColumn<string>(
                name: "gekozenHulpverlener",
                table: "afspraakModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gekozenHulpverlener",
                table: "afspraakModel");

            migrationBuilder.AddColumn<string>(
                name: "gekozenHulpverlenerId",
                table: "afspraakModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_afspraakModel_gekozenHulpverlenerId",
                table: "afspraakModel",
                column: "gekozenHulpverlenerId");

            migrationBuilder.AddForeignKey(
                name: "FK_afspraakModel_AspNetUsers_gekozenHulpverlenerId",
                table: "afspraakModel",
                column: "gekozenHulpverlenerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
