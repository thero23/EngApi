using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace English.Database.Migrations
{
    public partial class InitDictionaryWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictionaryWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DictionaryId = table.Column<Guid>(nullable: false),
                    WordId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryWords_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DictionaryWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryWords_DictionaryId",
                table: "DictionaryWords",
                column: "DictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryWords_WordId",
                table: "DictionaryWords",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictionaryWords");
        }
    }
}
