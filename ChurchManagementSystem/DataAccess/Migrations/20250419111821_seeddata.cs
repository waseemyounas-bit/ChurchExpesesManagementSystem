using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-ghij-klmnopqrstuv",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9486ddaa-d1a3-4ff8-9bd8-8d68220808b0", "AQAAAAIAAYagAAAAENa00lSoW9gjBwsGT9SRfzh9pAep3UZ46rGhnodMlTXv1Ql7XjU8fVwFf1DCF/qAvw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-ghij-klmnopqrstuv",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "29599b22-460b-4d31-a9cc-e571bcf5db72", "AQAAAAIAAYagAAAAEBhliVYIgY/YYh4+u9+GU3Hz80f63zpPJ6+xPULAvadwXGhhnmVOxWQBCphRYGxabQ==" });
        }
    }
}
