using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.Products.Dtos;
using MediatR;

namespace GastronoSys.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<Result<ProductDto>>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
