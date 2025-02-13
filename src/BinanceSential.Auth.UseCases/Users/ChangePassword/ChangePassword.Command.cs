namespace BinanceSential.Auth.UseCases.Users.ChangePassword;

public record ChangePasswordCommand(string UserId, string CurrentPassword, string NewPassword) : ICommand<Result>;
