using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "SaleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_CustomerId",
                table: "SaleItems",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Customers_CustomerId",
                table: "SaleItems",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Customers_CustomerId",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_CustomerId",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SaleItems");
        }
    }
}
