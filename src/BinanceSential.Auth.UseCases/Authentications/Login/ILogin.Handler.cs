using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Authentications.Login;

public interface ILoginHandler
{
  Task<Result<JwtDTO>> Handle(LoginCommand request, CancellationToken cancellationToken);
}
