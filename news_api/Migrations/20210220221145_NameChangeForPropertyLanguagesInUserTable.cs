using Microsoft.EntityFrameworkCore.Migrations;

namespace news_api.Migrations
{
    public partial class NameChangeForPropertyLanguagesInUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "AspNetUsers",
                newName: "Language");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "AspNetUsers",
                newName: "Languages");
        }
    }
}
