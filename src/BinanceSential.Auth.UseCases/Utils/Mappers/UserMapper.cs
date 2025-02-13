using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.Authentications.Register;
using BinanceSential.Auth.UseCases.DTOs;

namespace BinanceSential.Auth.UseCases.Utils.Mappers;

public static class UserMapper
{
  public static UserDTO ToDTO(User user) => new(
    user.Id,
    user.UserName ?? string.Empty,
    user.Email ?? string.Empty,
    user.PhoneNumber ?? string.Empty);

  public static IEnumerable<UserDTO> ToDTOs(IEnumerable<User> users) => users.Select(ToDTO);

  public static User ToEntity(RegisterCommand command) => new()
  {
    UserName = command.UserName,
    Email = command.Email,
    PhoneNumber = command.PhoneNumber
  };
}
