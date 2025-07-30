using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.StockItem.Dtos;
using MediatR;

namespace GastronoSys.Application.Features.StockItem.Commands
{
    public class CreateStockItemCommand : IRequest<Result<StockItemDto>>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }

    }

}
