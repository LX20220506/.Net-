using Microsoft.EntityFrameworkCore.Migrations;

namespace DBCreate.Migrations
{
    public partial class AddSexToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Students",
                type: "nvarchar(5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Students");
        }
    }
}
