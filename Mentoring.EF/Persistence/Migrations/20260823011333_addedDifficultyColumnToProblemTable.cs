using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoring.EF.Migrations
{
    /// <inheritdoc />
    public partial class addedDifficultyColumnToProblemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Problems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Problems");
        }
    }
}
