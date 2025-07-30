using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.Products.Commands;
using GastronoSys.Application.Features.Products.Dtos;
using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GastronoSys.Application.Features.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CreateProductCommandHandler> _logger;


        public CreateProductCommandHandler(IProductRepository repository, ILogger<CreateProductCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsByNameAsync(request.Name, cancellationToken);

            if (exists)
            {
                _logger.LogWarning("Duplicate product name attempted: {ProductName}", request.Name);
                return Result<ProductDto>.ValidationError(new List<string> { " product with this name is already available!" });
            }
            var entity = request.Adapt<Product>();



            entity.Name = request.Name;
            entity.Name = request.Name;

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            var created = await _repository.GetByIdWithCategoryAsync(entity.Id, cancellationToken);

            if (created is null)
            {
                _logger.LogError("Product created but not found in DB. ProductId: {ProductId}", entity.Id);
                return Result<ProductDto>.NotFound("Product could not be found after creationb");
            }

            var dto = created.Adapt<ProductDto>();
            _logger.LogInformation("Product created successfully: {@ProductDto}", dto);

            return Result<ProductDto>.Created(dto, "Product created successfully");
        }
    }
}
