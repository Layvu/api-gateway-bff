using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
    Task<Order?> GetByIdAsync(string id);
    Task CreateAsync(Order order);
}