using BinanceSential.Auth.Core.UserAggregate.ValueObjects;
using FluentValidation;

namespace BinanceSential.Auth.Web.Authentications;

public class LoginValidator : Validator<LoginRequest>
{
  public LoginValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress()
      .MinimumLength(EmailVO.MIN_LENGTH)
      .MaximumLength(EmailVO.MAX_LENGTH)
      .WithMessage(EmailVO.ERROR_MESSAGE);

    RuleFor(x => x.Password)
      .NotEmpty()
      .MinimumLength(PasswordVO.MIN_LENGTH)
      .MaximumLength(PasswordVO.MAX_LENGTH)
      .Matches(PasswordVO.REGEX)
      .WithMessage(PasswordVO.ERROR_MESSAGE);
  }
}
