using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingCompanyDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaCode",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "CompanyContact",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Floor",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "UnitNumber",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ZoneLocation",
                table: "Companies",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UEN",
                table: "Companies",
                column: "UEN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_UEN",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyContact",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UnitNumber",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ZoneLocation",
                table: "Companies");
        }
    }
}
