using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.Core.UserAggregate;

public class User : IdentityUser<Guid>, IAggregateRoot
{

}
