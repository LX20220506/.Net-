using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class AddHeadImgToUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeadImg",
                table: "UserInfo",
                type: "nvarchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadImg",
                table: "UserInfo");
        }
    }
}
