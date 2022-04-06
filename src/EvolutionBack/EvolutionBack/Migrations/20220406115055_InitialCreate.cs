using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Additions",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Additions", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPair = table.Column<bool>(type: "bit", nullable: false),
                    IsOnEnemy = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstPropertyUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecondPropertyUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Cards_Additions_AdditionUid",
                        column: x => x.AdditionUid,
                        principalTable: "Additions",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_Properties_FirstPropertyUid",
                        column: x => x.FirstPropertyUid,
                        principalTable: "Properties",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_Properties_SecondPropertyUid",
                        column: x => x.SecondPropertyUid,
                        principalTable: "Properties",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateTable(
                name: "InGameUsers",
                columns: table => new
                {
                    UserUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGameUsers", x => new { x.UserUid, x.RoomUid });
                    table.ForeignKey(
                        name: "FK_InGameUsers_Rooms_RoomUid",
                        column: x => x.RoomUid,
                        principalTable: "Rooms",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InGameUsers_Users_UserUid",
                        column: x => x.UserUid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InGameUserUserUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InGameUserRoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FoodCurrent = table.Column<int>(type: "int", nullable: false),
                    FoodMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Animals_InGameUsers_InGameUserUserUid_InGameUserRoomUid",
                        columns: x => new { x.InGameUserUserUid, x.InGameUserRoomUid },
                        principalTable: "InGameUsers",
                        principalColumns: new[] { "UserUid", "RoomUid" });
                });

            migrationBuilder.CreateTable(
                name: "AnimalProperty",
                columns: table => new
                {
                    AnimalsUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertiesUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalProperty", x => new { x.AnimalsUid, x.PropertiesUid });
                    table.ForeignKey(
                        name: "FK_AnimalProperty_Animals_AnimalsUid",
                        column: x => x.AnimalsUid,
                        principalTable: "Animals",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalProperty_Properties_PropertiesUid",
                        column: x => x.PropertiesUid,
                        principalTable: "Properties",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalProperty_PropertiesUid",
                table: "AnimalProperty",
                column: "PropertiesUid");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_InGameUserUserUid_InGameUserRoomUid",
                table: "Animals",
                columns: new[] { "InGameUserUserUid", "InGameUserRoomUid" });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AdditionUid",
                table: "Cards",
                column: "AdditionUid");

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

            migrationBuilder.CreateIndex(
                name: "IX_InGameUsers_RoomUid",
                table: "InGameUsers",
                column: "RoomUid");

            migrationBuilder.CreateIndex(
                name: "IX_InGameUsers_UserUid",
                table: "InGameUsers",
                column: "UserUid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalProperty");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Additions");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "InGameUsers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
