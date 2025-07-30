using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.StockItem.Commands;
using GastronoSys.Application.Features.StockItem.Dtos;
using GastronoSys.Domain.Repositories;
using Mapster;
using MediatR;

namespace GastronoSys.Application.Features.StockItem.Handlers
{
    public class CreateStockItemCommandHandler : IRequestHandler<CreateStockItemCommand, Result<StockItemDto>>
    {
        private readonly IStockItemRepository _stockItemRepository;
        public CreateStockItemCommandHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }
        public async Task<Result<StockItemDto>> Handle(CreateStockItemCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Adapt<Domain.Entities.StockItem>();

            await _stockItemRepository.AddAsync(entity);
            await _stockItemRepository.SaveChangesAsync();

            var dto = entity.Adapt<StockItemDto>();

            return Result<StockItemDto>.Created(dto, "Stock item created succeffuly");
        }
    }
}
