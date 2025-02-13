namespace BinanceSential.Auth.Web.Authentications;

public class LoginRequest
{
  /// <summary>
  /// The user email.
  /// </summary>
  public required string Email { get; set; }

  /// <summary>
  /// The user password.
  /// </summary>
  public required string Password { get; set; }
}
