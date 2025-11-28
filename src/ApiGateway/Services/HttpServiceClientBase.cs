using System.Text.Json;

namespace ApiGateway.Services;

public abstract class HttpServiceClientBase
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger Logger;
    protected readonly JsonSerializerOptions JsonOptions;

    protected HttpServiceClientBase(HttpClient client, ILogger logger)
    {
        HttpClient = client;
        Logger = logger;
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}