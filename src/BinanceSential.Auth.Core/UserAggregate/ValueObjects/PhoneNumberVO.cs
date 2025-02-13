namespace BinanceSential.Auth.Core.UserAggregate.ValueObjects;
public static class PhoneNumberVO
{
  public static readonly int MIN_LENGTH = 8;
  public static readonly int MAX_LENGTH = 32;
  public static readonly string REGEX = "^[0-9]*$";
  public static readonly string ERROR_MESSAGE = "The password must be between 8 and 32 characters long and contain only letters, numbers, and special characters.";
}
