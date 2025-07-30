using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.ProductIngredients.Dtos;
using MediatR;

namespace GastronoSys.Application.Features.ProductIngredients.Commands
{
    public class CreateProductIngredientCommand : IRequest<Result<ProductIngredientDto>>
    {
        public int ProductId { get; set; }
        public int StockItemId { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
