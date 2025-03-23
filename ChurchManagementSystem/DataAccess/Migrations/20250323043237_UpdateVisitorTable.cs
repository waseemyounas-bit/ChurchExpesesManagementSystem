using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVisitorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Purpose",
                table: "Visitors",
                newName: "Zipcode");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Visitors",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Visitors",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeChurch",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PicturePath",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "HomeChurch",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Visitors");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "Visitors",
                newName: "Purpose");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Visitors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Visitors",
                newName: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "PicturePath",
                table: "Members",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
