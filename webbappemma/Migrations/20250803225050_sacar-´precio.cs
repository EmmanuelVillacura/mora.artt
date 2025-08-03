using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbappemma.Migrations
{
    public partial class sacarprecio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Producto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Producto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
