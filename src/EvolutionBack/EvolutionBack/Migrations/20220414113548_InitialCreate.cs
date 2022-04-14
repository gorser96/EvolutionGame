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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxTimeLeft = table.Column<TimeSpan>(type: "time", nullable: true),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    IsStarted = table.Column<bool>(type: "bit", nullable: false),
                    IsPaused = table.Column<bool>(type: "bit", nullable: false),
                    PauseStartedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Additions",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsBase = table.Column<bool>(type: "bit", nullable: false),
                    RoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Additions", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Additions_Rooms_RoomUid",
                        column: x => x.RoomUid,
                        principalTable: "Rooms",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateTable(
                name: "InGameUsers",
                columns: table => new
                {
                    UserUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    StartStepTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsHost = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGameUsers", x => new { x.UserUid, x.RoomUid });
                    table.ForeignKey(
                        name: "FK_InGameUsers_AspNetUsers_UserUid",
                        column: x => x.UserUid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InGameUsers_Rooms_RoomUid",
                        column: x => x.RoomUid,
                        principalTable: "Rooms",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Additions_Name",
                table: "Additions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Additions_RoomUid",
                table: "Additions",
                column: "RoomUid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalProperty_PropertiesUid",
                table: "AnimalProperty",
                column: "PropertiesUid");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_InGameUserUserUid_InGameUserRoomUid",
                table: "Animals",
                columns: new[] { "InGameUserUserUid", "InGameUserRoomUid" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AssemblyName",
                table: "Properties",
                column: "AssemblyName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalProperty");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Additions");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "InGameUsers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
