using ApiGateway.Cache;
using ApiGateway.Services;

namespace ApiGateway.Features.Profile;

public class ProfileAggregator
{
    private readonly IUserServiceClient _userClient;
    private readonly IOrderServiceClient _orderClient;
    private readonly IProductServiceClient _productClient;
    private readonly ICacheService _cache;
    private readonly ILogger<ProfileAggregator> _logger;

    public ProfileAggregator(
        IUserServiceClient userClient,
        IOrderServiceClient orderClient,
        IProductServiceClient productClient,
        ICacheService cache,
        ILogger<ProfileAggregator> logger)
    {
        _userClient = userClient;
        _orderClient = orderClient;
        _productClient = productClient;
        _cache = cache;
        _logger = logger;
    }

    public async Task<ProfileResponse?> GetProfileAsync(string userId, CancellationToken ct)
    {
        // Проверка кэша
        var cacheKey = $"profile:{userId}";
        var cached = await _cache.GetAsync<ProfileResponse>(cacheKey, ct);
        if (cached is not null)
        {
            _logger.LogInformation("Cache hit for user {UserId}", userId);
            return cached;
        }

        _logger.LogInformation("Cache miss for user {UserId}. Aggregating data...", userId);

        // Параллельные запросы к микросервисам
        var userTask = _userClient.GetUserAsync(userId, ct);
        var ordersTask = _orderClient.GetOrdersAsync(userId, ct);
        var productsTask = _productClient.GetFavoriteProductsAsync(userId, ct);

        await Task.WhenAll(userTask, ordersTask, productsTask);

        // Если пользователь не найден возвращаем null
        if (userTask.Result is null)
        {
            _logger.LogWarning("User {UserId} not found", userId);
            return null;
        }

        // Агрегируем результат
        var result = new ProfileResponse(
            User: userTask.Result,
            Orders: ordersTask.Result ?? new List<OrderDto>(),
            FavoriteProducts: productsTask.Result ?? new List<ProductDto>()
        );

        // Кэшируем на 30 секунд
        await _cache.SetAsync(cacheKey, result, TimeSpan.FromSeconds(30), ct);
        _logger.LogInformation("Profile for user {UserId} cached for 30 seconds", userId);

        return result;
    }
}