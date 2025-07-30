using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.ProductIngredients.Commands;
using GastronoSys.Application.Features.ProductIngredients.Dtos;
using GastronoSys.Application.Resources;
using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace GastronoSys.Application.Features.ProductIngredients.Handlers
{
    public class CreatProductIngredientCommandHandler : IRequestHandler<CreateProductIngredientCommand, Result<ProductIngredientDto>>
    {
        private readonly IProductIngredientRepository _repository;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public CreatProductIngredientCommandHandler(IProductIngredientRepository repository, IStringLocalizer<ValidationMessages> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<Result<ProductIngredientDto>> Handle(CreateProductIngredientCommand request, CancellationToken cancellationToken)
        {
            var exits = await _repository.ExistsAsync(request.ProductId, request.StockItemId, cancellationToken);
            if (exits)
                return Result<ProductIngredientDto>.Conflict(_localizer["IngredientAlreadyUsed"]);


            var entity = request.Adapt<ProductIngredient>();

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync();

            var created = await _repository.GetByIdWithDetailAsync(entity.Id, cancellationToken);

            if (created is null)
                return Result<ProductIngredientDto>.NotFound(_localizer["IngredientNotFoundAfterCreation"]);

            var dto = created.Adapt<ProductIngredientDto>();

            return Result<ProductIngredientDto>.Created(dto, _localizer["IngredientCreatedSuccessfully"]);
        }
    }
}
