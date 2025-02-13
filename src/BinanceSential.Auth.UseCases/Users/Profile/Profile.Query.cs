using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Users.Profile;

public record ProfileQuery(string UserId) : IQuery<Result<UserDTO>>;
