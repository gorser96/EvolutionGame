using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InGameUsers_UserUid",
                table: "InGameUsers");

            migrationBuilder.DropIndex(
                name: "IX_GameHistoryUsers_UserUid",
                table: "GameHistoryUsers");

            migrationBuilder.DropIndex(
                name: "IX_Cards_FirstPropertyUid",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_SecondPropertyUid",
                table: "Cards");

            migrationBuilder.CreateIndex(
                name: "IX_InGameUsers_UserUid",
                table: "InGameUsers",
                column: "UserUid");

            migrationBuilder.CreateIndex(
                name: "IX_InGameCards_RoomUid",
                table: "InGameCards",
                column: "RoomUid");

            migrationBuilder.CreateIndex(
                name: "IX_InAnimalProperties_PropertyUid",
                table: "InAnimalProperties",
                column: "PropertyUid");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistoryUsers_UserUid",
                table: "GameHistoryUsers",
                column: "UserUid");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_FirstPropertyUid",
                table: "Cards",
                column: "FirstPropertyUid");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_SecondPropertyUid",
                table: "Cards",
                column: "SecondPropertyUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InGameUsers_UserUid",
                table: "InGameUsers");

            migrationBuilder.DropIndex(
                name: "IX_InGameCards_RoomUid",
                table: "InGameCards");

            migrationBuilder.DropIndex(
                name: "IX_InAnimalProperties_PropertyUid",
                table: "InAnimalProperties");

            migrationBuilder.DropIndex(
                name: "IX_GameHistoryUsers_UserUid",
                table: "GameHistoryUsers");

            migrationBuilder.DropIndex(
                name: "IX_Cards_FirstPropertyUid",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_SecondPropertyUid",
                table: "Cards");

            migrationBuilder.CreateIndex(
                name: "IX_InGameUsers_UserUid",
                table: "InGameUsers",
                column: "UserUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameHistoryUsers_UserUid",
                table: "GameHistoryUsers",
                column: "UserUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_FirstPropertyUid",
                table: "Cards",
                column: "FirstPropertyUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_SecondPropertyUid",
                table: "Cards",
                column: "SecondPropertyUid",
                unique: true,
                filter: "[SecondPropertyUid] IS NOT NULL");
        }
    }
}
