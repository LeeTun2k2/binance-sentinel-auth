using BinanceSential.Auth.Core.Interfaces.ITokenService;
using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.DTOs;
using BinanceSential.Auth.UseCases.Utils.Mappers;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.UseCases.Authentications.Refresh;

public class RefreshHandler(UserManager<User> userManager, IJwtService jwtService) : ICommandHandler<RefreshCommand, Result<JwtDTO>>, IRefreshHandler
{
  public async Task<Result<JwtDTO>> Handle(RefreshCommand request, CancellationToken cancellationToken)
  {
    // Find user
    var user = await userManager.FindByIdAsync(request.UserId);
    if (user == null)
      return Result.NotFound("User not found.");

    // Get user roles
    var roles = await userManager.GetRolesAsync(user) ?? [];

    // Generate token
    var token = await jwtService.GenerateToken(user, roles);

    var jwtDTO = JwtMapper.ToDTO(token);
    return Result.Success(jwtDTO);
  }
}
