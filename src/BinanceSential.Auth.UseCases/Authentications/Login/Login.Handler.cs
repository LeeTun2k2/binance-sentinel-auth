using BinanceSential.Auth.Core.Interfaces.ITokenService;
using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.DTOs;
using BinanceSential.Auth.UseCases.Utils.Mappers;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.UseCases.Authentications.Login;

public class LoginHandler(
  UserManager<User> userManager,
  SignInManager<User> signInManager,
  IJwtService jwtService) : ICommandHandler<LoginCommand, Result<JwtDTO>>, ILoginHandler
{
  public async Task<Result<JwtDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    // Find user
    var user = await userManager.FindByEmailAsync(request.Email);
    if (user == null)
      return Result.NotFound("User not found.");

    // Log in with password
    var result = await signInManager.PasswordSignInAsync(user, request.Password, false, true);

    // Handle sign in is not allowed
    if (result.IsNotAllowed)
      return Result.Forbidden("User is not allowed to sign in.");

    // Handle locked out
    if (result.IsLockedOut)
      return Result.Forbidden("User is locked out.");

    // Handle two factor authentication required
    if (result.RequiresTwoFactor)
      return Result.Forbidden("User requires two factor authentication.");

    // Handle invalid login
    if (!result.Succeeded)
      return Result.Unauthorized("Invalid email or password.");

    // Get user roles
    var roles = await userManager.GetRolesAsync(user) ?? [];

    // Generate token
    var token = await jwtService.GenerateToken(user, roles);

    var jwtDTO = JwtMapper.ToDTO(token);
    return Result.Success(jwtDTO);
  }
}
