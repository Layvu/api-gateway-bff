using ApiGateway.Features.Profile;

namespace ApiGateway.Services;

public interface IProductServiceClient
{
    Task<List<ProductDto>?> GetFavoriteProductsAsync(string userId, CancellationToken ct = default);
}