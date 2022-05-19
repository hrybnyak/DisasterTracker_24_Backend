using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "UserLocation");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserLocation");
        }
    }
}
