namespace BinanceSential.Auth.Web.Authentications;

public record LoginResponse(string AccessToken, string RefreshToken, DateTime Expiration);
