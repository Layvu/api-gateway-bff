namespace ApiGateway.Features.Profile;

public record ProfileResponse(
    UserDto User,
    IReadOnlyList<OrderDto> Orders,
    IReadOnlyList<ProductDto> FavoriteProducts);

public record UserDto(string Id, string Name, string Email);
public record OrderDto(string Id, DateTime Date, decimal Total);
public record ProductDto(string Id, string Name, decimal Price);