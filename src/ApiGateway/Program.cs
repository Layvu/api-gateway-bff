using ApiGateway.Extensions;
using ApiGateway.Features.Profile;
using ApiGateway.Services;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Логирование
builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration)
          .WriteTo.Console()
          .WriteTo.Seq(ctx.Configuration["Seq:ServerUrl"]!));

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.Window = TimeSpan.FromSeconds(10);
        limiterOptions.PermitLimit = 20;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 0;
    });
});

// Основные сервисы
builder.Services.AddHttpClient();
builder.Services.AddCustomCache(builder.Configuration);
builder.Services.AddScoped<ProfileAggregator>();

// Получаем URL-ы
var userServiceUrl = builder.Configuration["Microservices:UserApiUrl"] ?? "http://localhost:5001";
var orderServiceUrl = builder.Configuration["Microservices:OrderApiUrl"] ?? "http://localhost:5003";
var productServiceUrl = builder.Configuration["Microservices:ProductApiUrl"] ?? "http://localhost:5002";

// Регистрируем HTTP клиенты с политиками Polly
builder.Services.AddHttpClient<IUserServiceClient, UserServiceClient>(client =>
{
    client.BaseAddress = new Uri(userServiceUrl);
})
.AddPolicyHandler(PollyPolicies.GetRetryPolicy())
.AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IOrderServiceClient, OrderServiceClient>(client =>
{
    client.BaseAddress = new Uri(orderServiceUrl);
})
.AddPolicyHandler(PollyPolicies.GetRetryPolicy())
.AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(client =>
{
    client.BaseAddress = new Uri(productServiceUrl);
})
.AddPolicyHandler(PollyPolicies.GetRetryPolicy())
.AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

var app = builder.Build();

app.UseRateLimiter();

app.MapProfileEndpoints();

app.Run();