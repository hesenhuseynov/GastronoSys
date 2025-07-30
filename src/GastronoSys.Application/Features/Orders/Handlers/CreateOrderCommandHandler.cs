using GastronoSys.Application.Common.Results;
using GastronoSys.Application.Features.Orders.BusinessRules;
using GastronoSys.Application.Features.Orders.Commands;
using GastronoSys.Application.Features.Orders.Dtos;
using GastronoSys.Application.Resources;
using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Enums;
using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace GastronoSys.Application.Features.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
    {
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IProductRepository _productRepository;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly GastronoSysDbContext _context;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            ITableRepository tableRepository, IProductRepository productRepository,
            OrderBusinessRules orderBusinessRules,
            GastronoSysDbContext context, ILogger<CreateOrderCommandHandler> logger,
            IStringLocalizer<ValidationMessages> localizer)


        {
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
            _productRepository = productRepository;
            _orderBusinessRules = orderBusinessRules;
            _context = context;
            _logger = logger;
            _localizer = localizer;

        }

        public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(request.TableId);

            var productIds = request.OrderItems.Select(x => x.ProductId).Distinct().ToList();

            var products = await _productRepository.GetByIdsWithIngredientsAsync(productIds, cancellationToken);

            var businessErrors = _orderBusinessRules.Validate(request, products, table);

            if (businessErrors.Any())
            {
                var localizedErrors = businessErrors.Select(e => _localizer[e.Key, e.Args].Value).ToList();
                _logger.LogWarning("Order validation failed for request: {@Request}, Errors:{@Errors} ", request, localizedErrors);
                return Result<OrderDto>.ValidationError(localizedErrors);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var orderItem in request.OrderItems)
                {
                    var product = products.FirstOrDefault(p => p.Id == orderItem.ProductId);

                    if (product is null)
                    {
                        return Result<OrderDto>.ValidationError(new List<string> { _localizer["ProductNotFound", orderItem.ProductId].Value });
                    }

                    foreach (var ingredient in product.ProductIngredients)
                    {
                        var stockItem = ingredient.StockItem;
                        var totalRequired = ingredient.Quantity * orderItem.Quantity;
                        if (stockItem == null)

                            return Result<OrderDto>.ValidationError(new List<string> { _localizer["StockItemForIngredientNotFound", ingredient.Id].Value });

                        if (stockItem.Quantity < totalRequired)
                            return Result<OrderDto>.ValidationError(new List<string> {
                                _localizer["StockNotEnough", stockItem.Name, totalRequired, stockItem.Quantity].Value
                            });

                        stockItem.Quantity -= (int)totalRequired;

                        var movement = new StockMovement
                        {
                            StockItemId = stockItem.Id,
                            Quantity = (int)totalRequired,
                            StockMovementTypeId = (int)StockMovementTypeEnum.Out,
                            MovementDate = DateTime.UtcNow,
                            Note = $"Order #{orderItem.ProductId} üçün ingredient {stockItem.Name} çıxıldı"
                        };

                        _context.StockMovements.Add(movement);
                        _context.StockItems.Update(stockItem);
                    }
                }

                var order = request.Adapt<Order>();
                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveChangesAsync();

                await transaction.CommitAsync();

                var createdOrder = await _orderRepository.GetByIdWithDetailAsync(order.Id);

                if (createdOrder is null)
                {
                    _logger.LogError("Order created but not found in DB .  OrderId: {OrderId}", order.Id);
                    return Result<OrderDto>.NotFound(_localizer["OrderNotFoundAfterCreation"].Value);
                }

                var orderDto = createdOrder.Adapt<OrderDto>();
                _logger.LogInformation("Order created successfully: {@OrderDto}", orderDto);
                return Result<OrderDto>.Created(orderDto, "Order created succesfuly");
            }

            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogWarning(ex, "DbUpdateConcurrencyException: Order creation failed for request: {@Request}", request);
                return Result<OrderDto>.Conflict(_localizer["ConcurrencyConflict"].Value);
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Unexpected error while creating order. Request: {@Request}", request);
                return Result<OrderDto>.Failure(ResultStatus.Error, _localizer["OrderUnexpectedError"]);
            }
        }
    }
}
