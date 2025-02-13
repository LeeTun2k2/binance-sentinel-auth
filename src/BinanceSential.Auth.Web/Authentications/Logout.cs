using BinanceSential.Auth.UseCases.Authentications.Logout;

namespace BinanceSential.Auth.Web.Authentications;

public class Logout(IMediator mediator, IHttpContextAccessor httpContextAccessor) : EndpointWithoutRequest<ApiResponse<LogoutResponse>>
{
  public override void Configure()
  {
    Post("/authentication/logout");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId is null)
    {
      Response = new ApiResponse<LogoutResponse>(400, "Invalid email.");
      return;
    }

    await mediator.Send(new LogoutCommand(), cancellationToken);

    Response = new ApiResponse<LogoutResponse>(new LogoutResponse("Log out successfully."));
  }
}
