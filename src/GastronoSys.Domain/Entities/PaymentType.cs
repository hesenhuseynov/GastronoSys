namespace GastronoSys.Domain.Entities
{
    public class PaymentType : BaseEntity
    {
        public string Name { get; set; }  //Cash,Card ,etc 

        public string? Description { get; set; }
        public List<Order> Orders { get; set; }
        public List<Receipt> Receipts { get; set; }
    }
}
