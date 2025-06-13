using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "PurchaseItems",
                newName: "PurchaseItemId");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseItemId",
                table: "SaleItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Medicines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_PurchaseItemId",
                table: "SaleItems",
                column: "PurchaseItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_PurchaseItems_PurchaseItemId",
                table: "SaleItems",
                column: "PurchaseItemId",
                principalTable: "PurchaseItems",
                principalColumn: "PurchaseItemId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_PurchaseItems_PurchaseItemId",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_PurchaseItemId",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "PurchaseItemId",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "PurchaseItemId",
                table: "PurchaseItems",
                newName: "ItemId");

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "PurchaseItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "PurchaseItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "Medicines",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
