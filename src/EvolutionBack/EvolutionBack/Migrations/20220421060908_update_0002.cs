using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_InGameUsers_InGameUserUserUid_InGameUserRoomUid",
                table: "Animals");

            migrationBuilder.AlterColumn<Guid>(
                name: "InGameUserUserUid",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InGameUserRoomUid",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_InGameUsers_InGameUserUserUid_InGameUserRoomUid",
                table: "Animals",
                columns: new[] { "InGameUserUserUid", "InGameUserRoomUid" },
                principalTable: "InGameUsers",
                principalColumns: new[] { "UserUid", "RoomUid" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_InGameUsers_InGameUserUserUid_InGameUserRoomUid",
                table: "Animals");

            migrationBuilder.AlterColumn<Guid>(
                name: "InGameUserUserUid",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "InGameUserRoomUid",
                table: "Animals",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_InGameUsers_InGameUserUserUid_InGameUserRoomUid",
                table: "Animals",
                columns: new[] { "InGameUserUserUid", "InGameUserRoomUid" },
                principalTable: "InGameUsers",
                principalColumns: new[] { "UserUid", "RoomUid" });
        }
    }
}
