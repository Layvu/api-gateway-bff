using ApiGateway.Extensions;
using ApiGateway.Features.Profile;
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
builder.Services.AddCustomHttpClients(builder.Configuration);
builder.Services.AddScoped<ProfileAggregator>();

var app = builder.Build();

// до endpoint’ов
app.UseRateLimiter();

// Эндпоинты
app.MapProfileEndpoints();

app.Run();