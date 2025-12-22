using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
    Task<string> CreateOrderAsync(string userId, decimal totalAmount);
}