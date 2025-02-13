using System.Security.Claims;
using BinanceSential.Auth.UseCases.Users.ChangePassword;

namespace BinanceSential.Auth.Web.Users;

public class ChangePassword(IMediator mediator, IHttpContextAccessor httpContextAccessor) : Endpoint<ChangePasswordRequest, ApiResponse<ChangePasswordResponse>>
{
  public override void Configure()
  {
    Post("/user/change-password");
  }

  public override async Task HandleAsync(
    ChangePasswordRequest request,
    CancellationToken cancellationToken)
  {
    var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Sid)?.Value;
    if (userId is null)
    {
      Response = new ApiResponse<ChangePasswordResponse>(401, "Unauthorized.");
      return;
    }

    var result = await mediator.Send(new ChangePasswordCommand(userId, request.CurrentPassword, request.NewPassword), cancellationToken);

    if (!result.IsSuccess)
    {
      Response = new ApiResponse<ChangePasswordResponse>(400, "Failed to change password.", result.Errors);
      return;
    }

    var data = new ChangePasswordResponse("Password has bean changed successfully.");
    Response = new ApiResponse<ChangePasswordResponse>(data);
  }
}
