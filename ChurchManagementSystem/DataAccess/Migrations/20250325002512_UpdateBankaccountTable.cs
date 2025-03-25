using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBankaccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Donations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoutingNumber",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "RoutingNumber",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "Donations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Members_MemberId",
                table: "Donations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
