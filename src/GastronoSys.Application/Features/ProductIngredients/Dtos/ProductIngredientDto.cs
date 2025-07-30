namespace GastronoSys.Application.Features.ProductIngredients.Dtos
{
    public class ProductIngredientDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int StockItemId { get; set; }

        public string StockItemName { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }
    }

}
