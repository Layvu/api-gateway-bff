using ProductService.Domain.Models;

namespace ProductService.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task CreateAsync(Product product);
}