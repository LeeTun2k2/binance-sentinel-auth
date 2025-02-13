using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.DTOs;
using BinanceSential.Auth.UseCases.Utils.Mappers;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.UseCases.Users.Profile;

public class ProfileHandler(UserManager<User> userManager) : IQueryHandler<ProfileQuery, Result<UserDTO>>, IProfileHandler
{
  public async Task<Result<UserDTO>> Handle(ProfileQuery request, CancellationToken cancellationToken)
  {
    // Check if user exists
    var user = await userManager.FindByIdAsync(request.UserId);
    if (user is null)
      return Result.NotFound("User not found");

    // Return user data
    var userDTO = UserMapper.ToDTO(user);

    return Result.Success(userDTO);
  }
}
