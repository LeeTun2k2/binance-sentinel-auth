namespace BinanceSential.Auth.UseCases.DTOs;
public record JwtDTO(string AccessToken, string RefreshToken, DateTime Expiration);
