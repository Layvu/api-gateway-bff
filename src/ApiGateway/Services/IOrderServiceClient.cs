using ApiGateway.Features.Profile;

namespace ApiGateway.Services;

public interface IOrderServiceClient
{
    Task<List<OrderDto>?> GetOrdersAsync(string userId, CancellationToken ct = default);
}