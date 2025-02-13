using BinanceSential.Auth.Core.UserAggregate.ValueObjects;
using FluentValidation;

namespace BinanceSential.Auth.Web.Users;

public class ChangePasswordValidator : Validator<ChangePasswordRequest>
{
  public ChangePasswordValidator()
  {
    RuleFor(x => x.CurrentPassword)
      .NotEmpty()
      .MinimumLength(PasswordVO.MIN_LENGTH)
      .MaximumLength(PasswordVO.MAX_LENGTH)
      .Matches(PasswordVO.REGEX)
      .WithMessage(PasswordVO.ERROR_MESSAGE);

    RuleFor(x => x.NewPassword)
      .NotEmpty()
      .MinimumLength(PasswordVO.MIN_LENGTH)
      .MaximumLength(PasswordVO.MAX_LENGTH)
      .Matches(PasswordVO.REGEX)
      .WithMessage(PasswordVO.ERROR_MESSAGE);
  }
}
