namespace BinanceSential.Auth.Core.UserAggregate.ValueObjects;
public static class EmailVO
{
  public static readonly int MIN_LENGTH = 8;
  public static readonly int MAX_LENGTH = 128;
  public static readonly string ERROR_MESSAGE = "The email must be between 8 and 128 characters long.";
}
