using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Produts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id_Pro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nam_Pro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Des_Pro = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    Pri_Pro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Url_Pro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id_Pro);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
