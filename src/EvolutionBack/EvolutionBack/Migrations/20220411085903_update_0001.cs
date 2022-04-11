using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Rooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDateTime",
                table: "Rooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaused",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MaxTimeLeft",
                table: "Rooms",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StepNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "InGameUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartStepTime",
                table: "InGameUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomUid",
                table: "Additions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Additions_RoomUid",
                table: "Additions",
                column: "RoomUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Additions_Rooms_RoomUid",
                table: "Additions",
                column: "RoomUid",
                principalTable: "Rooms",
                principalColumn: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additions_Rooms_RoomUid",
                table: "Additions");

            migrationBuilder.DropIndex(
                name: "IX_Additions_RoomUid",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FinishedDateTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsPaused",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MaxTimeLeft",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "StepNumber",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "InGameUsers");

            migrationBuilder.DropColumn(
                name: "StartStepTime",
                table: "InGameUsers");

            migrationBuilder.DropColumn(
                name: "RoomUid",
                table: "Additions");
        }
    }
}
