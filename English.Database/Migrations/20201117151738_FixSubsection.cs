using Microsoft.EntityFrameworkCore.Migrations;

namespace English.Database.Migrations
{
    public partial class FixSubsection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lecture",
                table: "Subsections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lecture",
                table: "Subsections");
        }
    }
}
