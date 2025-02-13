namespace BinanceSential.Auth.UseCases.Users.ChangePassword;

public interface IChangePasswordHandler
{
  Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken);
}
