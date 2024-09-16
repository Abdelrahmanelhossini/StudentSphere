using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace domain_and_repo.Migrations
{
    /// <inheritdoc />
    public partial class onetomany_student_level_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_levels_Levelid",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_levels_Levelid",
                table: "Students",
                column: "Levelid",
                principalTable: "levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_levels_Levelid",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_levels_Levelid",
                table: "Students",
                column: "Levelid",
                principalTable: "levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
