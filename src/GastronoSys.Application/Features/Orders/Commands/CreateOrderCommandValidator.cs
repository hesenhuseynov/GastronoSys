using FluentValidation;
using GastronoSys.Application.Features.Orders.Dtos;

namespace GastronoSys.Application.Features.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.TableId).GreaterThan(0);
            RuleFor(x => x.OrderItems).NotEmpty();
            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemDtoValidator());
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
