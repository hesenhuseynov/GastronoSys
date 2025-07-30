using GastronoSys.Application.Features.Orders.Commands;
using GastronoSys.Application.Features.Orders.Dtos;
using GastronoSys.Domain.Entities;
using Mapster;

namespace GastronoSys.Application.Features.Orders.Profiles
{
    public class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateOrderCommand, Order>()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.OrderItems, src => src.OrderItems.Adapt<List<OrderItem>>());

            config.NewConfig<OrderItemDto, OrderItem>();

            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.TableName, src => src.Table.Name)
                .Map(dest => dest.CustomerName, src => src.Customer.FullName)
                .Map(dest => dest.OrderStatusName, src => src.OrderStatus.Name)
                .Map(dest => dest.PaymentTypeName, src => src.PaymentType.Name)
                .Map(dest => dest.OrderItems, src => src.OrderItems.Adapt<List<OrderItemDto>>());



            config.NewConfig<OrderItem, OrderItemDto>()
                .Map(dest => dest.ProductName, src => src.Product.Name)
                .Map(dest => dest.TotalPrice, src => src.Quantity * src.UnitPrice);

        }
    }
}
