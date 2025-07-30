namespace GastronoSys.Domain.Entities
{
    public class ProductIngredient : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StockItemId { get; set; }
        public StockItem StockItem { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
