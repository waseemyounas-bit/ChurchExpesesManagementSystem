using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDonationTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
                name: "FK_Donations_Members_MemberId",
                table: "Donations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Visitors_VisitorId",
                table: "Donations",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Visitors_VisitorId",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_VisitorId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Donations");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
