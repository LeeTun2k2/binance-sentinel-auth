using BinanceSential.Auth.Core.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.UseCases.Users.ChangePassword;

public class ChangePasswordHandler(UserManager<User> userManager) : ICommandHandler<ChangePasswordCommand, Result>, IChangePasswordHandler
{
  public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
  {
    // Find user
    var user = await userManager.FindByIdAsync(request.UserId);
    if (user == null)
      return Result.NotFound("User not found.");

    // Check password
    var checkPasswordResult = await userManager.CheckPasswordAsync(user, request.CurrentPassword);
    if (!checkPasswordResult)
      return Result.Unauthorized("Invalid password.");

    // Change password
    var changePasswordResult = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
    if (!changePasswordResult.Succeeded)
      return Result.Error(changePasswordResult.Errors.ToString());

    return Result.Success();
  }
}
