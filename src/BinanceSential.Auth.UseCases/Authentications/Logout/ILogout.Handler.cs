namespace BinanceSential.Auth.UseCases.Authentications.Logout;

public interface ILogoutHandler
{
  Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken);
}
