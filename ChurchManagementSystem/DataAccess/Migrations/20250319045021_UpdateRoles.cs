using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicaitonUsers_ApplicationRoles_RoleId",
                table: "ApplicaitonUsers");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicaitonUsers_IdentityRole_RoleId",
                table: "ApplicaitonUsers",
                column: "RoleId",
                principalTable: "IdentityRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicaitonUsers_IdentityRole_RoleId",
                table: "ApplicaitonUsers");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicaitonUsers_ApplicationRoles_RoleId",
                table: "ApplicaitonUsers",
                column: "RoleId",
                principalTable: "ApplicationRoles",
                principalColumn: "Id");
        }
    }
}
