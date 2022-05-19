using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class addUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "UserLocation");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserLocation");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserLocation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserLocation");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserLocation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserLocation",
                type: "text",
                nullable: true);
        }
    }
}
