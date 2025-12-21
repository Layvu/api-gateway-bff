using ApiGateway.Features.Profile;
using System.Net.Http.Json;

namespace ApiGateway.Services;

public class OrderServiceClient : HttpServiceClientBase, IOrderServiceClient
{
    public OrderServiceClient(HttpClient client, ILogger<OrderServiceClient> logger) 
        : base(client, logger) { }

    public async Task<List<OrderDto>?> GetOrdersAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            var response = await HttpClient.GetAsync($"/api/orders/user/{userId}", ct);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<OrderDto>();
            }

            response.EnsureSuccessStatusCode();

            var orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>(JsonOptions, ct);
            return orders ?? new List<OrderDto>();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting orders for user {UserId}", userId);
            return null;
        }
    }
}