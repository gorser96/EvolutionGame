using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Properties_FirstPropertyUid",
                table: "Cards");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalUid",
                table: "InGameCards",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHasFatTissue",
                table: "InAnimalProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "InGameCardUid",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_InGameCardUid_InGameUserRoomUid",
                table: "Animals",
                columns: new[] { "InGameCardUid", "InGameUserRoomUid" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_InGameCards_InGameCardUid_InGameUserRoomUid",
                table: "Animals",
                columns: new[] { "InGameCardUid", "InGameUserRoomUid" },
                principalTable: "InGameCards",
                principalColumns: new[] { "RoomUid", "CardUid" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Properties_FirstPropertyUid",
                table: "Cards",
                column: "FirstPropertyUid",
                principalTable: "Properties",
                principalColumn: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_InGameCards_InGameCardUid_InGameUserRoomUid",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Properties_FirstPropertyUid",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Animals_InGameCardUid_InGameUserRoomUid",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AnimalUid",
                table: "InGameCards");

            migrationBuilder.DropColumn(
                name: "IsHasFatTissue",
                table: "InAnimalProperties");

            migrationBuilder.DropColumn(
                name: "InGameCardUid",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Animals");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Properties_FirstPropertyUid",
                table: "Cards",
                column: "FirstPropertyUid",
                principalTable: "Properties",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
