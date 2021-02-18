using Microsoft.EntityFrameworkCore.Migrations;

namespace news_api.Migrations
{
    public partial class SourcesAndDomainsPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sources",
                table: "TopHeadLinesRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domains",
                table: "EverythingRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sources",
                table: "EverythingRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sources",
                table: "TopHeadLinesRequests");

            migrationBuilder.DropColumn(
                name: "Domains",
                table: "EverythingRequests");

            migrationBuilder.DropColumn(
                name: "Sources",
                table: "EverythingRequests");
        }
    }
}
