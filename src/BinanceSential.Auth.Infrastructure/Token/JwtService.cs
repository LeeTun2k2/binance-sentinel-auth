using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BinanceSential.Auth.Core.Interfaces.ITokenService;
using BinanceSential.Auth.Core.TokenAggregate;
using BinanceSential.Auth.Core.UserAggregate;
using Microsoft.IdentityModel.Tokens;

namespace BinanceSential.Auth.Infrastructure.Token;

public class JwtService(IConfiguration config) : IJwtService
{
  private readonly JwtConfig _jwtConfig = new(config);

  public async Task<Jwt> GenerateToken(User user, IList<string> roles)
  {
    var claims = GetClaims(user, roles);

    var accessToken = GenerateJwtToken(claims, _jwtConfig.AccessTokenExpiration);
    var refreshToken = GenerateJwtToken(claims, _jwtConfig.RefreshTokenExpiration);

    return await Task.FromResult(new Jwt(accessToken, refreshToken, DateTime.UtcNow.AddHours(_jwtConfig.AccessTokenExpiration)));
  }

  private List<Claim> GetClaims(User user, IList<string> roles)
  {
    var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
        new(ClaimTypes.Uri, _jwtConfig.Issuer)
    };

    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

    return claims;
  }

  private string GenerateJwtToken(List<Claim> claims, int expirationHours)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _jwtConfig.Issuer,
        audience: _jwtConfig.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddHours(expirationHours),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
