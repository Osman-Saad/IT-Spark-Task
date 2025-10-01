using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITSpark.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Items",
                newName: "UnitPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Items",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
