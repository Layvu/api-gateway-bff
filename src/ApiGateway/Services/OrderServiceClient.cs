using ApiGateway.Features.Profile;

namespace ApiGateway.Services;

public class OrderServiceClient : HttpServiceClientBase, IOrderServiceClient
{
    public OrderServiceClient(HttpClient client, ILogger<OrderServiceClient> logger) 
        : base(client, logger) { }

    public async Task<List<OrderDto>?> GetOrdersAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            // Временная заглушка
            await Task.Delay(100, ct);
            return new List<OrderDto>
            {
                new OrderDto("1", DateTime.UtcNow.AddDays(-1), 100.50m),
                new OrderDto("2", DateTime.UtcNow.AddDays(-5), 200.00m)
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting orders for user {UserId}", userId);
            return null;
        }
    }
}