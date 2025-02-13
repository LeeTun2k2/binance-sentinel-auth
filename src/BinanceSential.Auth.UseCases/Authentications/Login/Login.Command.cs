using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Authentications.Login;

public record LoginCommand(string Email, string Password) : ICommand<Result<JwtDTO>>;
