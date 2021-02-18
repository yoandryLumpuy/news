using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace news_api.Migrations
{
    public partial class CreatedAtAddedToTableEverythingRequestsAndTopheadlinesRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TopHeadLinesRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EverythingRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TopHeadLinesRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EverythingRequests");
        }
    }
}
