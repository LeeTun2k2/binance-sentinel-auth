namespace BinanceSential.Auth.Web.Authentications;

public class RegisterRequest
{
  /// <summary>
  /// The user name.
  /// </summary>
  public required string UserName { get; set; }

  /// <summary>
  /// The user email.
  /// </summary>
  public required string Email { get; set; }

  /// <summary>
  /// The user password.
  /// </summary>
  public required string Password { get; set; }

  /// <summary>
  /// The user phone number.
  /// </summary>
  public required string PhoneNumber { get; set; }
}
