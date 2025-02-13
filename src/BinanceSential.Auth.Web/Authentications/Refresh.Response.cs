namespace BinanceSential.Auth.Web.Authentications;

public record RefreshResponse(string accessToken, string refreshToken, DateTime expiration);
