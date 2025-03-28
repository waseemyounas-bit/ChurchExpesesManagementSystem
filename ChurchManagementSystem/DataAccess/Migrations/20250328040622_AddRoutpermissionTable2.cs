using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRoutpermissionTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutPermissions_AspNetRoles_RolId1",
                table: "RoutPermissions");

            migrationBuilder.DropIndex(
                name: "IX_RoutPermissions_RolId1",
                table: "RoutPermissions");

            migrationBuilder.DropColumn(
                name: "RolId1",
                table: "RoutPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "RolId",
                table: "RoutPermissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_RoutPermissions_RolId",
                table: "RoutPermissions",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutPermissions_AspNetRoles_RolId",
                table: "RoutPermissions",
                column: "RolId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutPermissions_AspNetRoles_RolId",
                table: "RoutPermissions");

            migrationBuilder.DropIndex(
                name: "IX_RoutPermissions_RolId",
                table: "RoutPermissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RolId",
                table: "RoutPermissions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RolId1",
                table: "RoutPermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RoutPermissions_RolId1",
                table: "RoutPermissions",
                column: "RolId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutPermissions_AspNetRoles_RolId1",
                table: "RoutPermissions",
                column: "RolId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
