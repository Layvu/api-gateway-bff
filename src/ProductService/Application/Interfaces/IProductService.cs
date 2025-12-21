using ProductService.Domain.Models;

namespace ProductService.Application.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<string> CreateProductAsync(string name, decimal price);
}