using Microsoft.EntityFrameworkCore.Migrations;

namespace School.API.Migrations
{
    public partial class updatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Courses_CourseId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_CourseId",
                table: "Teachers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
