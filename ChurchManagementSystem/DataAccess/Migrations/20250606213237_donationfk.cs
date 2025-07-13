using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class donationfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-ghij-klmnopqrstuv",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "46a655b5-efad-4bab-8129-f0d05b51c844", "AQAAAAIAAYagAAAAEEdLhvU8Lw6ulLk+4RPLzK8Ea6YEENVsD0t0W7WhuejVRp1eeiPzy2z8cTjFukkZow==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-ghij-klmnopqrstuv",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f4d7cb00-2e97-4bb7-addf-a03f53495048", "AQAAAAIAAYagAAAAEHAuxexzlFZP3XPEEh7SqIhdzdznngPjmPoZ/kqxkp1tZnNe77IgADpIp3UdRnqNsg==" });
        }
    }
}
