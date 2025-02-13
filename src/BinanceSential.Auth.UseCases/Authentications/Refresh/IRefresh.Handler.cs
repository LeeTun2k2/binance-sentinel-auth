using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Authentications.Refresh;

public interface IRefreshHandler
{
  Task<Result<JwtDTO>> Handle(RefreshCommand request, CancellationToken cancellationToken);
}
