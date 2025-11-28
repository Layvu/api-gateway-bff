using ApiGateway.Features.Profile;

namespace ApiGateway.Services;

public class ProductServiceClient : HttpServiceClientBase, IProductServiceClient
{
    public ProductServiceClient(HttpClient client, ILogger<ProductServiceClient> logger) 
        : base(client, logger) { }

    public async Task<List<ProductDto>?> GetFavoriteProductsAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            // Временная заглушка
            await Task.Delay(100, ct);
            return new List<ProductDto>
            {
                new ProductDto("1", "Laptop", 999.99m),
                new ProductDto("2", "Mouse", 25.50m)
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting favorite products for user {UserId}", userId);
            return null;
        }
    }
}