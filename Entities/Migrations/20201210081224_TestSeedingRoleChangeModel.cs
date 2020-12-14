using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class TestSeedingRoleChangeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Testik",
                table: "UserRoles",
                nullable: true);

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name", "Testik" },
                values: new object[] { new Guid("b79eb9a4-f929-47b7-a849-30efe3c0a3a5"), "admin", null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name", "Testik" },
                values: new object[] { new Guid("cea9ae1e-3406-4af1-9457-348ff5550f82"), "user", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("b79eb9a4-f929-47b7-a849-30efe3c0a3a5"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("cea9ae1e-3406-4af1-9457-348ff5550f82"));

            migrationBuilder.DropColumn(
                name: "Testik",
                table: "UserRoles");
        }
    }
}
