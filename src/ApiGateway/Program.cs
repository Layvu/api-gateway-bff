using ApiGateway.Extensions;
using ApiGateway.Features.Profile;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Логирование
builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration)
          .WriteTo.Console()
          .WriteTo.Seq(ctx.Configuration["Seq:ServerUrl"]!));

// Основные сервисы
builder.Services.AddHttpClient();
builder.Services.AddCustomCache(builder.Configuration);
builder.Services.AddCustomHttpClients(builder.Configuration);
builder.Services.AddScoped<ProfileAggregator>();

var app = builder.Build();

// Только один эндпоинт
app.MapProfileEndpoints();

app.Run();