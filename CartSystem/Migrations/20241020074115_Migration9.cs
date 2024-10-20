using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartSystem.Migrations
{
    public partial class Migration9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing column
            migrationBuilder.DropColumn(
                name: "Quantity", // Column name to drop
                table: "OrdersItems" // Table name
            );

            // Add a new column with the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "Quantity", // Column name to add
                table: "OrdersItems", // Table name
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // IDENTITY specification
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the column added in Up method
            migrationBuilder.DropColumn(
                name: "Quantity", // Column name to drop
                table: "OrdersItems" // Table name
            );

            // Re-add the original column without IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Quantity", // Column name to add
                table: "OrdersItems", // Table name
                nullable: false,
                defaultValue: 0); // No IDENTITY in Down method
        }
    }
}
