using GastronoSys.Application.Features.StockItem.Commands;
using GastronoSys.Application.Features.StockItem.Dtos;
using Mapster;


namespace GastronoSys.Application.Features.StockItem.Profiles
{
    public class StockItemMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<CreateStockItemCommand, Domain.Entities.StockItem>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.StockMovements)
                .Ignore(dest => dest.ProductIngredients);



            config.NewConfig<Domain.Entities.StockItem, StockItemDto>();
        }
    }
}
