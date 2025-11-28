using ApiGateway.Features.Profile;

namespace ApiGateway.Services;

public interface IUserServiceClient
{
    Task<UserDto?> GetUserAsync(string userId, CancellationToken ct = default);
}