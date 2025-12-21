using ApiGateway.Features.Profile;
using System.Net.Http.Json;

namespace ApiGateway.Services;

public class ProductServiceClient : HttpServiceClientBase, IProductServiceClient
{
    public ProductServiceClient(HttpClient client, ILogger<ProductServiceClient> logger) 
        : base(client, logger) { }

    public async Task<List<ProductDto>?> GetFavoriteProductsAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            var response = await HttpClient.GetAsync("/api/products", ct);

            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>(JsonOptions, ct);
            return products ?? new List<ProductDto>();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting products for user {UserId}", userId);
            return null;
        }
    }
}