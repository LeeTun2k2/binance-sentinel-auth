using BinanceSential.Auth.Core.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.UseCases.Authentications.Logout;

public class LogoutHandler(SignInManager<User> signInManager) : ICommandHandler<LogoutCommand, Result>, ILogoutHandler
{
  public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
  {
    await signInManager.SignOutAsync();
    return Result.Success();
  }
}
