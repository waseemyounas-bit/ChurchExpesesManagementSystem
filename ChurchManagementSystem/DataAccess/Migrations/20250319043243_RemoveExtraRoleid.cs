using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtraRoleid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicaitonUsers_ApplicationRoles_RoleId1",
                table: "ApplicaitonUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicaitonUsers_RoleId1",
                table: "ApplicaitonUsers");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "ApplicaitonUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "ApplicaitonUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicaitonUsers_RoleId",
                table: "ApplicaitonUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicaitonUsers_ApplicationRoles_RoleId",
                table: "ApplicaitonUsers",
                column: "RoleId",
                principalTable: "ApplicationRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicaitonUsers_ApplicationRoles_RoleId",
                table: "ApplicaitonUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicaitonUsers_RoleId",
                table: "ApplicaitonUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "ApplicaitonUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "ApplicaitonUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicaitonUsers_RoleId1",
                table: "ApplicaitonUsers",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicaitonUsers_ApplicationRoles_RoleId1",
                table: "ApplicaitonUsers",
                column: "RoleId1",
                principalTable: "ApplicationRoles",
                principalColumn: "Id");
        }
    }
}
