using BinanceSential.Auth.Core.TokenAggregate;
using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Utils.Mappers;

public static class JwtMapper
{
  public static JwtDTO ToDTO(Jwt jwt) => new(jwt.AccessToken, jwt.RefreshToken, jwt.Expiration);
}
