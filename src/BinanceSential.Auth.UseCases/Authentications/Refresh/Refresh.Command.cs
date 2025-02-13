using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Authentications.Refresh;

public record RefreshCommand(string UserId) : ICommand<Result<JwtDTO>>;
