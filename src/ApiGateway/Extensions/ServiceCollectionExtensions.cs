using ApiGateway.Cache;
using ApiGateway.Services;
using Polly;
using ApiGateway.Extensions;

namespace ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var servicesConfig = configuration.GetSection("Services");

        services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
            client.BaseAddress = new Uri(servicesConfig["UserService"]!))
            .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicies.GetFallbackPolicy());

        services.AddHttpClient<IOrderServiceClient, OrderServiceClient>(client =>
            client.BaseAddress = new Uri(servicesConfig["OrderService"]!))
            .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicies.GetFallbackPolicy());

        services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
            client.BaseAddress = new Uri(servicesConfig["ProductService"]!))
            .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicies.GetFallbackPolicy());

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