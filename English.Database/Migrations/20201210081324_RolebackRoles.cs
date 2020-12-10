using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace English.Database.Migrations
{
    public partial class RolebackRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b00285f0-e579-4e0b-a3f1-794e574f54e2"), "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c2396ad9-9504-40f4-8a39-bbcb2ec8da8b"), "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("b00285f0-e579-4e0b-a3f1-794e574f54e2"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2396ad9-9504-40f4-8a39-bbcb2ec8da8b"));

            migrationBuilder.AddColumn<string>(
                name: "Testik",
                table: "UserRoles",
                type: "nvarchar(max)",
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
    }
}
