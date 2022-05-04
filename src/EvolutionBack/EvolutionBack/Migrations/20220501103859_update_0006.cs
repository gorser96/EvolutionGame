using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Additions_Rooms_RoomUid",
                table: "Additions");

            migrationBuilder.DropIndex(
                name: "IX_Additions_RoomUid",
                table: "Additions");

            migrationBuilder.DropColumn(
                name: "RoomUid",
                table: "Additions");

            migrationBuilder.CreateTable(
                name: "AdditionRoom",
                columns: table => new
                {
                    AdditionsUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomsUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionRoom", x => new { x.AdditionsUid, x.RoomsUid });
                    table.ForeignKey(
                        name: "FK_AdditionRoom_Additions_AdditionsUid",
                        column: x => x.AdditionsUid,
                        principalTable: "Additions",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionRoom_Rooms_RoomsUid",
                        column: x => x.RoomsUid,
                        principalTable: "Rooms",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionRoom_RoomsUid",
                table: "AdditionRoom",
                column: "RoomsUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionRoom");

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
    }
}
