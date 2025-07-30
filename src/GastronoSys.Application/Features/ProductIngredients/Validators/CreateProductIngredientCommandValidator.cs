using FluentValidation;
using GastronoSys.Application.Features.ProductIngredients.Commands;

namespace GastronoSys.Application.Features.ProductIngredients.Validators
{
    public class CreateProductIngredientCommandValidator : AbstractValidator<CreateProductIngredientCommand>
    {

        public CreateProductIngredientCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId should be selected");

            RuleFor(x => x.StockItemId).GreaterThan(0).WithMessage("StockItemId should be selected");

            RuleFor(x => x.Unit).NotEmpty();

            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than zero");
        }
    }
}
