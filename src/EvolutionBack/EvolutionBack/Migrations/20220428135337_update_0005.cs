using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameHistory",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistory", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "GameHistoryUsers",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameHistoryUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistoryUsers", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_GameHistoryUsers_AspNetUsers_UserUid",
                        column: x => x.UserUid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameHistoryUsers_GameHistory_GameHistoryUid",
                        column: x => x.GameHistoryUid,
                        principalTable: "GameHistory",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameHistoryUsers_GameHistoryUid",
                table: "GameHistoryUsers",
                column: "GameHistoryUid");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistoryUsers_UserUid",
                table: "GameHistoryUsers",
                column: "UserUid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameHistoryUsers");

            migrationBuilder.DropTable(
                name: "GameHistory");
        }
    }
}
