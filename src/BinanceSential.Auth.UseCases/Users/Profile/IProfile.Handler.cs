using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Users.Profile;

public interface IProfileHandler
{
  Task<Result<UserDTO>> Handle(ProfileQuery request, CancellationToken cancellationToken);
}
