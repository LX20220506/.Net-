using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Goods.EF.Migrations.Log
{
    public partial class LoginAddPropLoginTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "LoginLog");

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginTime",
                table: "LoginLog",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginTime",
                table: "LoginLog");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "LoginLog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
