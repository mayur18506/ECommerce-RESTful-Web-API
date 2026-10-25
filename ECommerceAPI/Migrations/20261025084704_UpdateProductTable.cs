using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Products",
                newName: "Category");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Products",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
