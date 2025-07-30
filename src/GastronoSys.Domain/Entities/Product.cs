namespace GastronoSys.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public bool IsActive { get; set; } = true;

        public List<OrderItem> OrderItems { get; set; }

        //public List<StockItem> StockItems { get; set; }
        public List<ProductIngredient> ProductIngredients { get; set; }

        public List<Menu> Menus { get; set; }

    }
}
