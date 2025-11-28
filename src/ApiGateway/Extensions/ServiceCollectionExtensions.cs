using ApiGateway.Cache;
using ApiGateway.Services;

namespace ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var servicesConfig = configuration.GetSection("Services");

        services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
            client.BaseAddress = new Uri(servicesConfig["UserService"]!));

        services.AddHttpClient<IOrderServiceClient, OrderServiceClient>(client =>
            client.BaseAddress = new Uri(servicesConfig["OrderService"]!));

        services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
            client.BaseAddress = new Uri(servicesConfig["ProductService"]!));

        return services;
    }

    public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
        });

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }
}