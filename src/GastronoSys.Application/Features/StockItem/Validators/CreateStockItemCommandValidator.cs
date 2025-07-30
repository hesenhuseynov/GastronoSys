using FluentValidation;
using GastronoSys.Application.Features.StockItem.Commands;

namespace GastronoSys.Application.Features.StockItem.Validators
{
    public class CreateStockItemCommandValidator : AbstractValidator<CreateStockItemCommand>
    {
        public CreateStockItemCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Quantity).GreaterThan(0);

            RuleFor(x => x.Unit).NotEmpty();
        }
    }
}
