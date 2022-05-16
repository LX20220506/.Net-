using Microsoft.EntityFrameworkCore.Migrations;

namespace School.API.Migrations
{
    public partial class updatetable_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "Grade",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Grade");
        }
    }
}
