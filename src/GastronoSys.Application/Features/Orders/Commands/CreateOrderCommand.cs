using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.Orders.Dtos;
using MediatR;

namespace GastronoSys.Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Result<OrderDto>>
    {
        public int TableId { get; set; }
        public int? CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public int OrderStatusId { get; set; }
        public int? PaymentTypeId { get; set; }
        public string? Note { get; set; }
    }
}
