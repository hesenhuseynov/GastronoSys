namespace GastronoSys.Domain.Entities
{
    public class StockItem : BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }

        //public int ProductId { get; set; }

        //public Product Product { get; set; }

        public List<ProductIngredient> ProductIngredients { get; set; }

        public List<StockMovement> StockMovements { get; set; }
    }
}
