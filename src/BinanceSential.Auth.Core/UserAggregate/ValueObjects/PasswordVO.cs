namespace BinanceSential.Auth.Core.UserAggregate.ValueObjects;
public static class PasswordVO
{
  public static readonly int MIN_LENGTH = 8;
  public static readonly int MAX_LENGTH = 32;
  public static readonly string REGEX = "^[a-zA-Z0-9!@#$%^&*()\\-_=+<>.,?/|\\\\]*$";
  public static readonly string ERROR_MESSAGE = "The phone number must be between 8 and 32 characters long and contain only numbers.";
}
