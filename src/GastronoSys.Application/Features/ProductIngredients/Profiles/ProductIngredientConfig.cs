using GastronoSys.Application.Features.ProductIngredients.Commands;
using GastronoSys.Application.Features.ProductIngredients.Dtos;
using GastronoSys.Domain.Entities;
using Mapster;

namespace GastronoSys.Application.Features.ProductIngredients.Profiles
{
    public class ProductIngredientConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateProductIngredientCommand, ProductIngredient>()
             .Ignore(c => c.StockItem)
             .Ignore(c => c.Product);


            config.NewConfig<ProductIngredient, ProductIngredientDto>()
                .Map(c => c.ProductName, c => c.Product.Name)
                .Map(c => c.StockItemName, c => c.StockItem.Name);
        }
    }
}
