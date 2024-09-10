using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace domain_and_repo.Migrations
{
    /// <inheritdoc />
    public partial class onetomany_student_level_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_levels_LevelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "Students",
                newName: "Levelid");

            migrationBuilder.RenameIndex(
                name: "IX_Students_LevelId",
                table: "Students",
                newName: "IX_Students_Levelid");

            migrationBuilder.AlterColumn<int>(
                name: "Levelid",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_levels_Levelid",
                table: "Students",
                column: "Levelid",
                principalTable: "levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_levels_Levelid",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Levelid",
                table: "Students",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Levelid",
                table: "Students",
                newName: "IX_Students_LevelId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_levels_LevelId",
                table: "Students",
                column: "LevelId",
                principalTable: "levels",
                principalColumn: "LevelId");
        }
    }
}
