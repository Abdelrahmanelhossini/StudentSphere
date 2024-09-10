using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace domain_and_repo.Migrations
{
    /// <inheritdoc />
    public partial class onetomany_student_level : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LevelID",
                table: "levels",
                newName: "LevelId");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_LevelId",
                table: "Students",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_levels_LevelId",
                table: "Students",
                column: "LevelId",
                principalTable: "levels",
                principalColumn: "LevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_levels_LevelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_LevelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "levels",
                newName: "LevelID");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
