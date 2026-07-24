using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoring.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedCodeLinekAsIndexInSubmissionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Submissions_CodeLink",
                table: "Submissions",
                column: "CodeLink");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_CodeLink",
                table: "Submissions");
        }
    }
}
