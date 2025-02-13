
using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Authentications.Register;

public interface IRegisterHandler
{
  Task<Result<JwtDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken);
}
