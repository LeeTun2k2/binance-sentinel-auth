using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Authentications.Register;

public record RegisterCommand(string UserName, string Email, string PhoneNumber, string Password, List<string> Roles) : ICommand<Result<JwtDTO>>;
