namespace BinanceSential.Auth.Infrastructure.Token;

public class JwtConfig(IConfiguration config)
{
  public string SecretKey { get; } = config["Token:Bearer:SecretKey"] ?? string.Empty;
  public string Issuer { get; } = config["Token:Bearer:Issuer"] ?? string.Empty;
  public string Audience { get; } = config["Token:Bearer:Audience"] ?? string.Empty;
  public string[] ValidAudiences { get; } = config.GetSection("Token:Bearer:ValidAudiences").Get<string[]>() ?? [];
  public string[] ValidIssuers { get; } = config.GetSection("Token:Bearer:ValidIssuers").Get<string[]>() ?? [];
  public int AccessTokenExpiration { get; } = config.GetValue("Token:Bearer:AccessTokenExpiration", 1);
  public int RefreshTokenExpiration { get; } = config.GetValue("Token:Bearer:RefreshTokenExpiration", 2);
}
