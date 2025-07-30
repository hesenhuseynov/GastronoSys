using GastronoSys.Application.Common.Errors;
using GastronoSys.Application.Features.Orders.Commands;
using GastronoSys.Domain.Entities;

namespace GastronoSys.Application.Features.Orders.BusinessRules
{
    public class OrderBusinessRules
    {
        public List<BusinessError> Validate(CreateOrderCommand request, List<Product> products, Table table)
        {
            var errors = new List<BusinessError>();

            if (table is null)
                errors.Add(new BusinessError("TableNotFound"));

            foreach (var item in request.OrderItems)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                if (product is null)
                    errors.Add(new BusinessError("ProductNotFound", item.ProductId));
                else
                {
                    foreach (var ingredient in product.ProductIngredients)
                    {
                        var totalRequired = ingredient.Quantity * item.Quantity;
                        var stockItem = ingredient.StockItem;

                        if (stockItem == null)
                            errors.Add(new BusinessError("StockItemIngredientNotFound", ingredient.Id));
                        else if (stockItem.Quantity < totalRequired)
                            errors.Add(new BusinessError("StockNotEnough", stockItem.Name, totalRequired, stockItem.Quantity));
                    }
                }
            }

            return errors;
        }
    }
}
