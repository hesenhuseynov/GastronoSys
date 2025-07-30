using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastronoSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintProductIngredientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductIngredient_ProductId",
                table: "ProductIngredient");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredient_ProductId_StockItemId",
                table: "ProductIngredient",
                columns: new[] { "ProductId", "StockItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductIngredient_ProductId_StockItemId",
                table: "ProductIngredient");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredient_ProductId",
                table: "ProductIngredient",
                column: "ProductId");
        }
    }
}
