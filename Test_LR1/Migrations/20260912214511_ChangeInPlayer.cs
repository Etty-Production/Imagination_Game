using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_LR1.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HP",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HP",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Players");
        }
    }
}
