using BinanceSential.Auth.UseCases.Authentications.Refresh;

namespace BinanceSential.Auth.Web.Authentications;

public class Refresh(IMediator mediator, IHttpContextAccessor httpContextAccessor) : EndpointWithoutRequest<ApiResponse<RefreshResponse>>
{
  public override void Configure()
  {
    Post("/authentication/refresh");
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId is null)
    {
      Response = new ApiResponse<RefreshResponse>(404, "User not found");
      return;
    }

    var result = await mediator.Send(new RefreshCommand(userId), cancellationToken);

    var data = new RefreshResponse(result.Value.AccessToken, result.Value.RefreshToken, result.Value.Expiration);
    Response = new ApiResponse<RefreshResponse>(data);
  }
}
