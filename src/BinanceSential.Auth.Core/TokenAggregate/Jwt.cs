namespace BinanceSential.Auth.Core.TokenAggregate;
public record Jwt(string AccessToken, string RefreshToken, DateTime Expiration);
