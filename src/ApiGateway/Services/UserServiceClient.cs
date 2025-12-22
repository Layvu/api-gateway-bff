using ApiGateway.Features.Profile;
using System.Net.Http.Json;

namespace ApiGateway.Services;

public class UserServiceClient : HttpServiceClientBase, IUserServiceClient
{
    public UserServiceClient(HttpClient client, ILogger<UserServiceClient> logger) 
        : base(client, logger) { }

    public async Task<UserDto?> GetUserAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            var response = await HttpClient.GetAsync($"/api/users/{userId}", ct);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Logger.LogWarning("User {UserId} not found in remote service", userId);
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserDto>(JsonOptions, ct);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting user {UserId}", userId);
            return null;
        }
    }
}