using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository repository, ILogger<OrderService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<string> CreateOrderAsync(string userId, decimal totalAmount)
    {
        var newOrder = new Order(Guid.NewGuid().ToString(), userId, totalAmount);
        await _repository.CreateAsync(newOrder);
        
        _logger.LogInformation("Order created: {Id} for user {UserId}", newOrder.Id, userId);
        return newOrder.Id;
    }
}