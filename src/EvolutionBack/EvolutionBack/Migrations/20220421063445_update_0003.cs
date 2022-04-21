using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionBack.Migrations
{
    public partial class update_0003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalProperty");

            migrationBuilder.CreateTable(
                name: "InAnimalProperties",
                columns: table => new
                {
                    PropertyUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimalUid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InAnimalProperties", x => new { x.PropertyUid, x.AnimalUid });
                    table.ForeignKey(
                        name: "FK_InAnimalProperties_Animals_AnimalUid",
                        column: x => x.AnimalUid,
                        principalTable: "Animals",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InAnimalProperties_Properties_PropertyUid",
                        column: x => x.PropertyUid,
                        principalTable: "Properties",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InAnimalProperties_AnimalUid",
                table: "InAnimalProperties",
                column: "AnimalUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InAnimalProperties");

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
        }
    }
}
