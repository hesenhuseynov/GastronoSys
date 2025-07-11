namespace GastronoSys.Domain.Entities
{
    public class StockMovementType : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<StockMovement> StockMovements { get; set; }
    }
}
