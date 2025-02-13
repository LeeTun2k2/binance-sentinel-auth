using BinanceSential.Auth.UseCases.Users.Profile;

namespace BinanceSential.Auth.Web.Users;

public class Profile(IMediator mediator, IHttpContextAccessor httpContextAccessor) : EndpointWithoutRequest<ApiResponse<ProfileResponse>>
{
  public override void Configure()
  {
    Get("/user/profile");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId is null)
    {
      Response = new ApiResponse<ProfileResponse>(401, "Unauthorized.");
      return;
    }

    var result = await mediator.Send(new ProfileQuery(userId), cancellationToken);

    if (!result.IsSuccess)
    {
      Response = new ApiResponse<ProfileResponse>(400, "Failed to change password.", result.Errors);
      return;
    }

    var data = new ProfileResponse(result.Value.Id, result.Value.UserName, result.Value.Email, result.Value.PhoneNumber);
    Response = new ApiResponse<ProfileResponse>(data);
  }
}
