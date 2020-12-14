using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddLinkSectionAndSubsections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "Subsections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subsections_SectionId",
                table: "Subsections",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsections_Sections_SectionId",
                table: "Subsections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsections_Sections_SectionId",
                table: "Subsections");

            migrationBuilder.DropIndex(
                name: "IX_Subsections_SectionId",
                table: "Subsections");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Subsections");
        }
    }
}
