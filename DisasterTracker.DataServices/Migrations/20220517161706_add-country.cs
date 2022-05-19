using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class addcountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ISO3 = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LongName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryDisaster",
                columns: table => new
                {
                    CountriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisastersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDisaster", x => new { x.CountriesId, x.DisastersId });
                    table.ForeignKey(
                        name: "FK_CountryDisaster_Country_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryDisaster_Disaster_DisastersId",
                        column: x => x.DisastersId,
                        principalTable: "Disaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryDisaster_DisastersId",
                table: "CountryDisaster",
                column: "DisastersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryDisaster");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
