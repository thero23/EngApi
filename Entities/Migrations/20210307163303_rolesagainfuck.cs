using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class rolesagainfuck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7650461a-2dce-4555-b7e2-96b89ea2de42", "a30ef222-a42e-4109-a2cc-b86ff985c328", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "622fb086-c855-43d7-a86d-386cf9368c48", "a6bbe032-2f92-42c0-8780-8269421330ce", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "380e2697-47ab-44b4-a3e8-21ddadc5d94e", "941aa22d-56b4-4b1e-8c6b-bd81f687b394", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "380e2697-47ab-44b4-a3e8-21ddadc5d94e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "622fb086-c855-43d7-a86d-386cf9368c48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7650461a-2dce-4555-b7e2-96b89ea2de42");
        }
    }
}
