using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class addcountrydisaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryDisaster_Country_CountriesId",
                table: "CountryDisaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryDisaster_Disaster_DisastersId",
                table: "CountryDisaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryDisaster",
                table: "CountryDisaster");

            migrationBuilder.RenameColumn(
                name: "DisastersId",
                table: "CountryDisaster",
                newName: "DisasterId");

            migrationBuilder.RenameColumn(
                name: "CountriesId",
                table: "CountryDisaster",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryDisaster_DisastersId",
                table: "CountryDisaster",
                newName: "IX_CountryDisaster_DisasterId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CountryDisaster",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "AffectedPopulation",
                table: "CountryDisaster",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CountryDisaster",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CountryDisaster",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryDisaster",
                table: "CountryDisaster",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryDisaster_CountryId",
                table: "CountryDisaster",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryDisaster_Country_CountryId",
                table: "CountryDisaster",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryDisaster_Disaster_DisasterId",
                table: "CountryDisaster",
                column: "DisasterId",
                principalTable: "Disaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryDisaster_Country_CountryId",
                table: "CountryDisaster");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryDisaster_Disaster_DisasterId",
                table: "CountryDisaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryDisaster",
                table: "CountryDisaster");

            migrationBuilder.DropIndex(
                name: "IX_CountryDisaster_CountryId",
                table: "CountryDisaster");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CountryDisaster");

            migrationBuilder.DropColumn(
                name: "AffectedPopulation",
                table: "CountryDisaster");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CountryDisaster");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CountryDisaster");

            migrationBuilder.RenameColumn(
                name: "DisasterId",
                table: "CountryDisaster",
                newName: "DisastersId");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "CountryDisaster",
                newName: "CountriesId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryDisaster_DisasterId",
                table: "CountryDisaster",
                newName: "IX_CountryDisaster_DisastersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryDisaster",
                table: "CountryDisaster",
                columns: new[] { "CountriesId", "DisastersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CountryDisaster_Country_CountriesId",
                table: "CountryDisaster",
                column: "CountriesId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryDisaster_Disaster_DisastersId",
                table: "CountryDisaster",
                column: "DisastersId",
                principalTable: "Disaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
