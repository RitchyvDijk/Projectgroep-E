using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations
{
    public partial class client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nickname = table.Column<string>(type: "TEXT", nullable: false),
                    Leeftijdscategorie = table.Column<string>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", nullable: false),
                    Hulpverlener = table.Column<string>(type: "TEXT", nullable: true),
                    Wachtwoord = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienten", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clienten");
        }
    }
}
