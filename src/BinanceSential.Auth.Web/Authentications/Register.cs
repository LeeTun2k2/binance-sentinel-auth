using BinanceSential.Auth.UseCases.Authentications.Register;

namespace BinanceSential.Auth.Web.Authentications;

public class Register(IMediator mediator) : Endpoint<RegisterRequest, ApiResponse<RegisterResponse>>
{
  public override void Configure()
  {
    Post("/authentication/register");
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    RegisterRequest request,
    CancellationToken cancellationToken)
  {
    var result = await mediator.Send(new RegisterCommand(request.UserName, request.Email, request.PhoneNumber, request.Password, ["User"]), cancellationToken);

    if (!result.IsSuccess)
    {
      var data = new RegisterResponse(string.Join('\n', result.Errors));
      Response = new ApiResponse<RegisterResponse>(data);
      return;
    }

    Response = new ApiResponse<RegisterResponse>(new RegisterResponse("User created successfully."));
  }
}
