using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations.AfspraakDb
{
    public partial class fixGebJaarBSN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jongerDan16",
                table: "afspraakModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "jongerDan16",
                table: "afspraakModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
