using BinanceSential.Auth.Core.TokenAggregate;
using BinanceSential.Auth.Core.UserAggregate;

namespace BinanceSential.Auth.Core.Interfaces.ITokenService;
public interface IJwtService
{
  public Task<Jwt> GenerateToken(User user, IList<string> roles);
}
