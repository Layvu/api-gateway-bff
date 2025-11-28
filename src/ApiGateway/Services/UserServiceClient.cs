using ApiGateway.Features.Profile;

namespace ApiGateway.Services;

public class UserServiceClient : HttpServiceClientBase, IUserServiceClient
{
    public UserServiceClient(HttpClient client, ILogger<UserServiceClient> logger) 
        : base(client, logger) { }

    public async Task<UserDto?> GetUserAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            // Временная заглушка
            // В реальности тут будет HTTP вызов к /api/users/{userId}
            await Task.Delay(100, ct); // Имитация задержки сети
            return new UserDto(userId, "John Doe", "john.doe@example.com");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error getting user {UserId}", userId);
            return null;
        }
    }
}