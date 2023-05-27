using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class IdentityNewModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TESTASAS",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TESTASAS",
                table: "AspNetUsers");
        }
    }
}
