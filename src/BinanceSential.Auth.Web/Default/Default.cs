namespace BinanceSential.Auth.Web.Default;

/// <summary>
/// Check server is running.
/// </summary>
/// <remarks>
/// Take status running and current time.
/// </remarks>
public class Default : EndpointWithoutRequest<ApiResponse<DefaultResponse>>
{
  public override void Configure()
  {
    Get("/");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Response.Data = new DefaultResponse();
    await SendOkAsync(Response, cancellationToken);
  }
}
