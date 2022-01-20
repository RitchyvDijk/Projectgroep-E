using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations
{
    public partial class changedclient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hulpverlener",
                table: "Clienten");

            migrationBuilder.AddColumn<int>(
                name: "HulpverlenerId",
                table: "Clienten",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HulpverlenerId",
                table: "Clienten");

            migrationBuilder.AddColumn<string>(
                name: "Hulpverlener",
                table: "Clienten",
                type: "TEXT",
                nullable: true);
        }
    }
}
