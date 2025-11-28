namespace ApiGateway.Features.Profile;

public static class ProfileEndpoint
{
    public static IEndpointRouteBuilder MapProfileEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/profile/{userId}", async (string userId, ProfileAggregator aggregator, CancellationToken ct) =>
        {
            if (string.IsNullOrEmpty(userId))
                return Results.BadRequest("User ID is required");

            var result = await aggregator.GetProfileAsync(userId, ct);
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        return app;
    }
}