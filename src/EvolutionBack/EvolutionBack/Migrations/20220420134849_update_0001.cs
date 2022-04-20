using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InGameCards",
                columns: table => new
                {
                    RoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGameCards", x => new { x.RoomUid, x.CardUid });
                    table.ForeignKey(
                        name: "FK_InGameCards_Cards_CardUid",
                        column: x => x.CardUid,
                        principalTable: "Cards",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InGameCards_Rooms_RoomUid",
                        column: x => x.RoomUid,
                        principalTable: "Rooms",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InGameCards_CardUid",
                table: "InGameCards",
                column: "CardUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InGameCards");
        }
    }
}
