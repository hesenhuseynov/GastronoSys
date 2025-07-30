namespace GastronoSys.Application.Features.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
