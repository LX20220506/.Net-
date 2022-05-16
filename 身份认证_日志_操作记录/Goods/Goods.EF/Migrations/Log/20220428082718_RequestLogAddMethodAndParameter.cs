using Microsoft.EntityFrameworkCore.Migrations;

namespace Goods.EF.Migrations.Log
{
    public partial class RequestLogAddMethodAndParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "RequestLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parameter",
                table: "RequestLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Method",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "Parameter",
                table: "RequestLog");
        }
    }
}
