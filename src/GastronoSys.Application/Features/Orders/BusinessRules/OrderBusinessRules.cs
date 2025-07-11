using GastronoSys.Application.Features.Orders.Commands;
using GastronoSys.Domain.Entities;

namespace GastronoSys.Application.Features.Orders.BusinessRules
{
    public class OrderBusinessRules
    {
        public List<string> Validate(CreateOrderCommand request, List<Product> products, Table table)
        {
            var errors = new List<string>();

            if (table is null)
                errors.Add("Table Not Found");

            foreach (var item in request.OrderItems)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                if (product is null)
                    errors.Add($"Product {item.ProductId} not found!");

                else
                {
                    var totalStock = product.StockItems?.Sum(si => si.Quantity) ?? 0;
                    if (totalStock < item.Quantity)
                        errors.Add($"Product  `{product.Name}` stock not enough ");
                }
            }

            return errors;
        }
    }
}
