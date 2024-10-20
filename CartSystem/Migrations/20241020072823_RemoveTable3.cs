using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartSystem.Migrations
{
    public partial class RemoveTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "OrdersItems"
           );

            migrationBuilder.DropTable(
                name: "Orders"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "OrdersItems"
           );

            migrationBuilder.DropTable(
                name: "Orders"
            );
        }
    }
}
