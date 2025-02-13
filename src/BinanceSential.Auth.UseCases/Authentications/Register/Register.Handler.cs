using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.DTOs;
using BinanceSential.Auth.UseCases.Utils.Mappers;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.UseCases.Authentications.Register;

public class RegisterHandler(UserManager<User> userManager) : ICommandHandler<RegisterCommand, Result<JwtDTO>>, IRegisterHandler
{
  public async Task<Result<JwtDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    // Check if user already exists
    var existingUser = await userManager.FindByEmailAsync(request.Email);
    if (existingUser != null)
      return Result.NotFound("User already exists.");

    // Create user
    var user = UserMapper.ToEntity(request);
    var result = await userManager.CreateAsync(user, request.Password);

    // Check if user creation was not successful
    if (!result.Succeeded)
    {
      string errors = string.Join(", ", result.Errors.Select(e => e.Description));
      return Result.Error($"User creation failed: {errors}");
    }

    // Add roles
    foreach (var role in request.Roles)
    {
      var identityResult = await userManager.AddToRoleAsync(user, role);
      if (!identityResult.Succeeded)
      {
        string errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
        return Result.Error($"Adding role {role} failed: {errors}");
      }
    }

    return Result.Success();
  }
}
