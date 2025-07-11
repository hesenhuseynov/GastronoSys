namespace GastronoSys.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public List<Product> Products { get; set; }
    }
}
