using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Visitors_VisitorId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_VisitorId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DonationTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DonationTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DonationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DonationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitorId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_VisitorId",
                table: "Donations",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Visitors_VisitorId",
                table: "Donations",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }
    }
}
