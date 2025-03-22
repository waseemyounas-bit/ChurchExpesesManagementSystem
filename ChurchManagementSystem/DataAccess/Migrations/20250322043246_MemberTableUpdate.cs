using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MemberTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Members",
                newName: "Zipcode");

            migrationBuilder.AlterColumn<bool>(
                name: "PriorMembership",
                table: "Members",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "BaptismalDate",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaptismalDate",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "Members",
                newName: "Name");

            migrationBuilder.AlterColumn<bool>(
                name: "PriorMembership",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
