using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApiId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AutoExpire = table.Column<bool>(type: "boolean", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisasterImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    MapImageAddress = table.Column<string>(type: "text", nullable: true),
                    InfrastructureMapImageAddress = table.Column<string>(type: "text", nullable: true),
                    OverviewMapImageAddress = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisasterImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisasterImage_Disaster_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "Disaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisasterStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Population0_14Affected = table.Column<int>(type: "integer", nullable: true),
                    Population15_64Affected = table.Column<int>(type: "integer", nullable: true),
                    PopulationAbove65Affected = table.Column<int>(type: "integer", nullable: true),
                    TotalPopulation = table.Column<int>(type: "integer", nullable: true),
                    CapitalExposed = table.Column<long>(type: "bigint", nullable: true),
                    Hospitals = table.Column<int>(type: "integer", nullable: true),
                    Schools = table.Column<int>(type: "integer", nullable: true),
                    Households = table.Column<int>(type: "integer", nullable: true),
                    Severity = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisasterStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisasterStatistics_Disaster_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "Disaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disaster_ApiId",
                table: "Disaster",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_Disaster_ApiId_LastUpdateDate",
                table: "Disaster",
                columns: new[] { "ApiId", "LastUpdateDate" });

            migrationBuilder.CreateIndex(
                name: "IX_DisasterImage_DisasterId",
                table: "DisasterImage",
                column: "DisasterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisasterStatistics_DisasterId",
                table: "DisasterStatistics",
                column: "DisasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisasterImage");

            migrationBuilder.DropTable(
                name: "DisasterStatistics");

            migrationBuilder.DropTable(
                name: "Disaster");
        }
    }
}
