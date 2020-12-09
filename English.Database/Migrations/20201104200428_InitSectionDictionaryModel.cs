using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace English.Database.Migrations
{
    public partial class InitSectionDictionaryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SectionDictionaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SectionId = table.Column<Guid>(nullable: false),
                    DictionaryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionDictionaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionDictionaries_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionDictionaries_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SectionDictionaries_DictionaryId",
                table: "SectionDictionaries",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionDictionaries_SectionId",
                table: "SectionDictionaries",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionDictionaries");
        }
    }
}
