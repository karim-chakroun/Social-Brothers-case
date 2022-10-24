using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social_Brothers_case.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adress",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    street = table.Column<string>(type: "TEXT", nullable: false),
                    houseNumber = table.Column<double>(type: "REAL", nullable: false),
                    zipCode = table.Column<string>(type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adress", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adress");
        }
    }
}
