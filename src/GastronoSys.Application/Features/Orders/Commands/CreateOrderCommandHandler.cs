using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.Orders.BusinessRules;
using GastronoSys.Application.Features.Orders.Dtos;
using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Enums;
using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastronoSys.Application.Features.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
    {
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        private readonly IOrderRepository _orderRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IProductRepository _productRepository;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly GastronoSysDbContext _context;
        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            ITableRepository tableRepository, IProductRepository productRepository,
            OrderBusinessRules orderBusinessRules, GastronoSysDbContext context, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
            _productRepository = productRepository;
            _orderBusinessRules = orderBusinessRules;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var table = await _tableRepository.GetByIdAsync(request.TableId);

            var productIds = request.OrderItems.Select(x => x.ProductId).Distinct().ToList();

            var products = await _productRepository.GetByIdAsync(productIds);

            var errors = _orderBusinessRules.Validate(request, products, table);

            if (errors.Any())
            {
                _logger.LogWarning("Order validation failed for request: {@Request}, Errors:{@Errors} ", request, errors);
                return Result<OrderDto>.ValidationError(errors);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var stockItems = await _context.StockItems.Where(si => productIds.Contains(si.ProductId))
                    .ToListAsync();

                foreach (var item in request.OrderItems)
                {
                    var stockItem = stockItems.FirstOrDefault(si => si.ProductId == item.ProductId);

                    if (stockItem is null)
                    {
                        _logger.LogWarning("Stock item not found for productId: {ProductId}", item.ProductId);

                        return Result<OrderDto>.ValidationError(new List<string> { $"stock  for product {item.ProductId} not found! " });
                    }

                    if (stockItem.Quantity < item.Quantity)
                    {
                        return Result<OrderDto>.ValidationError(new List<string> { $"Insufficient stock: '{stockItem.ProductId}'" });
                    }

                    stockItem.Quantity -= item.Quantity;

                    var movement = new StockMovement
                    {
                        StockItemId = stockItem.Id,
                        Quantity = item.Quantity,
                        StockMovementTypeId = (int)StockMovementTypeEnum.Out,
                        MovementDate = DateTime.UtcNow,
                        Note = $"Order #{item.ProductId} was deducted from stock"
                    };

                    _context.StockMovements.Add(movement);
                    _context.StockItems.Update(stockItem);

                }

                var order = request.Adapt<Order>();
                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveChangesAsync();




                await transaction.CommitAsync();

                var createdOrder = await _orderRepository.GetByIdWithDetailAsync(order.Id);

                if (createdOrder is null)
                {
                    _logger.LogError("Order created but not found in DB .  OrderId: {OrderId}", order.Id);
                    return Result<OrderDto>.NotFound("Order could  not be found after creation");

                }

                var orderDto = createdOrder.Adapt<OrderDto>();
                _logger.LogInformation("Order created successfully: {@OrderDto}", orderDto);
                return Result<OrderDto>.Created(orderDto, "Order created succesfuly");



            }

            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogWarning(ex, "DbUpdateConcurrencyException: Order creation failed for request: {@Request}", request);
                return Result<OrderDto>.Conflict("The information has changed, check again.");
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Unexpected error while creating order. Request: {@Request}", request);
                return Result<OrderDto>.Failure(ResultStatus.Error, "An error occured:" + ex.Message);
            }
        }
    }
}
