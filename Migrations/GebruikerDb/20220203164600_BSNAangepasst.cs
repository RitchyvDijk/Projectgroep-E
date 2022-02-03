using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.GebruikerDb
{
    public partial class BSNAangepasst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "gekozenTijd",
                table: "afspraakModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
