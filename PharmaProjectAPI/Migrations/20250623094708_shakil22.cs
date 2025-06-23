using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class shakil22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Customers_CustomerId",
                table: "SaleItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Customers_CustomerId",
                table: "SaleItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Customers_CustomerId",
                table: "SaleItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Customers_CustomerId",
                table: "SaleItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
