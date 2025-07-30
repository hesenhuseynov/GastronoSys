using GastronoSys.Application.Features.Products.Commands;
using GastronoSys.Application.Features.Products.Dtos;
using GastronoSys.Domain.Entities;
using Mapster;

namespace GastronoSys.Application.Features.Products.Profiles
{
    public class ProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateProductCommand, Product>()
                   .Ignore(dest => dest.Id)
                   .Ignore(dest => dest.CreatedAt)
                   .Ignore(dest => dest.UpdatedAt)
                   .Ignore(dest => dest.ProductCategory)
                   .Ignore(dest => dest.OrderItems)
                   .Ignore(dest => dest.ProductIngredients)
                   .Ignore(dest => dest.Menus);

            config.NewConfig<Product, ProductDto>()
                .Map(dest => dest.ProductCategoryName, src => src.ProductCategory.Name);
        }
    }
}
