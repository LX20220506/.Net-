using Microsoft.EntityFrameworkCore.Migrations;

namespace School.API.Migrations
{
    public partial class updatetable_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Teachers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CourseId",
                table: "Teachers",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Courses_CourseId",
                table: "Teachers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Courses_CourseId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_CourseId",
                table: "Teachers");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
