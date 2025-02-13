using BinanceSential.Auth.UseCases.Authentications.Login;

namespace BinanceSential.Auth.Web.Authentications;

public class Login(IMediator mediator) : Endpoint<LoginRequest, ApiResponse<LoginResponse>>
{
  public override void Configure()
  {
    Post("/authentication/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    LoginRequest request,
    CancellationToken cancellationToken)
  {
    var result = await mediator.Send(new LoginCommand(request.Email, request.Password), cancellationToken);

    if (!result.IsSuccess)
    {
      Response = new ApiResponse<LoginResponse>(400, "Failed to login.", result.Errors);
      return;
    }

    var data = new LoginResponse(result.Value.AccessToken, result.Value.RefreshToken, result.Value.Expiration);
    Response = new ApiResponse<LoginResponse>(data);
  }
}
