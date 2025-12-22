using ProductService.Application.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Product?> GetProductByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<string> CreateProductAsync(string name, decimal price)
    {
        var newProduct = new Product(Guid.NewGuid().ToString(), name, price);
        await _repository.CreateAsync(newProduct);
        
        _logger.LogInformation("Product created: {Id}", newProduct.Id);
        return newProduct.Id;
    }
}