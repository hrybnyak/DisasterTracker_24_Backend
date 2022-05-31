using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterTracker.DataServices.Migrations
{
    public partial class multiplesubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_UserPushSubscription_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPushSubscription_UserId",
                table: "UserPushSubscription", 
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
