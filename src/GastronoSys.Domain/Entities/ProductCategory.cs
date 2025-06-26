namespace GastronoSys.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public int SortOrder { get; set; }
        public List<Product> Products { get; set; }

    }
}
