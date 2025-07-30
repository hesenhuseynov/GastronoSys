using FluentValidation;
using GastronoSys.Application.Features.Products.Commands;

namespace GastronoSys.Application.Features.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero ");

            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0).WithMessage("Product category must be selected");
        }
    }
}
