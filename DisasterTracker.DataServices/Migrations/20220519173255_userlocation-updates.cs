using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class userlocationupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserLocation");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "UserLocation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UserLocation",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "UserLocation");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UserLocation");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserLocation",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
