using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seeddataadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-ghij-klmnopqrstuv",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f4d7cb00-2e97-4bb7-addf-a03f53495048", "AQAAAAIAAYagAAAAEHAuxexzlFZP3XPEEh7SqIhdzdznngPjmPoZ/kqxkp1tZnNe77IgADpIp3UdRnqNsg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-ghij-klmnopqrstuv",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9486ddaa-d1a3-4ff8-9bd8-8d68220808b0", "AQAAAAIAAYagAAAAENa00lSoW9gjBwsGT9SRfzh9pAep3UZ46rGhnodMlTXv1Ql7XjU8fVwFf1DCF/qAvw==" });
        }
    }
}
