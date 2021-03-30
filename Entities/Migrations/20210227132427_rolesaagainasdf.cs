using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class rolesaagainasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7900b37f-fb51-407a-a4bb-825587a79a1c", "225c44e5-fe06-42e5-8ee9-3fd81baf33be", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f589d47e-db01-4db4-8187-c9b09c6f908c", "07c51cd8-48d8-40b7-99f8-50a916ed649e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5cde56cd-5c12-4c0b-8d02-b1bbe8b82818", "9d41d83c-ace5-4ef7-af0f-606eebc4645d", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cde56cd-5c12-4c0b-8d02-b1bbe8b82818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7900b37f-fb51-407a-a4bb-825587a79a1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f589d47e-db01-4db4-8187-c9b09c6f908c");
        }
    }
}
