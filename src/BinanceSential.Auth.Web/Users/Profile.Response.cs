namespace BinanceSential.Auth.Web.Users;

public record ProfileResponse(Guid Id, string UserName, string Email, string PhoneNumber);
