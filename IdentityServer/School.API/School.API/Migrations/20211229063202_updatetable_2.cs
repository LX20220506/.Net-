using Microsoft.EntityFrameworkCore.Migrations;

namespace School.API.Migrations
{
    public partial class updatetable_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Grade",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Grade",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_CourseId",
                table: "Grade",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentId",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Courses_CourseId",
                table: "Grade",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Students_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Courses_CourseId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_StudentId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_CourseId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_StudentId",
                table: "Grade");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
