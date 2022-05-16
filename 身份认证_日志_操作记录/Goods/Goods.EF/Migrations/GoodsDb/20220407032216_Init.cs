using Microsoft.EntityFrameworkCore.Migrations;

namespace Goods.EF.Migrations.GoodsDb
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GoodsPrice = table.Column<decimal>(type: "money", maxLength: 8, nullable: false),
                    GoodsDesc = table.Column<string>(type: "text", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");
        }
    }
}
