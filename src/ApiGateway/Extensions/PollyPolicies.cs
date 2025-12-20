using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace ApiGateway.Extensions;

public static class PollyPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
            .HandleTransientHttpError() // 5xx + 408 + HttpRequestException
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (outcome, timespan, retryAttempt, ctx) =>
                {
                    // Serilog автоматически подхватит ILogger из DI
                });

    public static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        => Policy<HttpResponseMessage>
            .Handle<Exception>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .FallbackAsync(
                fallbackAction: ct =>
                {
                    var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent("{}")
                    };
                    return Task.FromResult(response);
                });
}
