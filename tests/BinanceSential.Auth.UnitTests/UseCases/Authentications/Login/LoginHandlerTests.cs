using Ardalis.Result;
using BinanceSential.Auth.Core.Interfaces.ITokenService;
using BinanceSential.Auth.Core.TokenAggregate;
using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.Authentications.Login;
using BinanceSential.Auth.UseCases.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BinanceSential.Auth.UnitTests.UseCases.Authentications.Login;

public class LoginHandlerTests
{
  private readonly Mock<UserManager<User>> _userManagerMock;
  private readonly Mock<SignInManager<User>> _signInManagerMock;
  private readonly Mock<IJwtService> _jwtServiceMock;
  private readonly LoginHandler _handler;

  public LoginHandlerTests()
  {
    _userManagerMock = new Mock<UserManager<User>>(
        Mock.Of<IUserStore<User>>(), null!, null!, null!, null!, null!, null!, null!, null!);

    _signInManagerMock = new Mock<SignInManager<User>>(
        _userManagerMock.Object,
        Mock.Of<IHttpContextAccessor>(),
        Mock.Of<IUserClaimsPrincipalFactory<User>>(),
        null!, null!, null!, null!);

    _jwtServiceMock = new Mock<IJwtService>();

    _handler = new LoginHandler(
        _userManagerMock.Object,
        _signInManagerMock.Object,
        _jwtServiceMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnNotFound_WhenUserDoesNotExist()
  {
    // Arrange
    var command = new LoginCommand("test@example.com", "P@ssw0rd");
    _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email))
        .ReturnsAsync((User)null!);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.Equal(ResultStatus.NotFound, result.Status);
  }

  [Fact]
  public async Task Handle_ShouldReturnForbidden_WhenSignInNotAllowed()
  {
    // Arrange
    var user = new User();
    var command = new LoginCommand("test@example.com", "password123");
    _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email))
        .ReturnsAsync(user);
    _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(user, command.Password, false, true))
        .ReturnsAsync(SignInResult.NotAllowed);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.Equal(ResultStatus.Forbidden, result.Status);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccess_WhenLoginIsValid()
  {
    // Arrange
    var user = new User();
    var command = new LoginCommand("test@example.com", "password123");
    var jwt = new Jwt("accessToken", "refreshToken", DateTime.UtcNow.AddHours(1));
    var jwtDTO = new JwtDTO("accessToken", "refreshToken", DateTime.UtcNow.AddHours(1));
    _userManagerMock.Setup(um => um.FindByEmailAsync(command.Email))
        .ReturnsAsync(user);
    _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(user, command.Password, false, true))
        .ReturnsAsync(SignInResult.Success);
    _userManagerMock.Setup(um => um.GetRolesAsync(user))
        .ReturnsAsync(["User"]);
    _jwtServiceMock.Setup(js => js.GenerateToken(user, It.IsAny<IList<string>>()))
        .ReturnsAsync(jwt);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.Equal(ResultStatus.Ok, result.Status);
    Assert.NotNull(result.Value);
    Assert.Equal("accessToken", result.Value.AccessToken);
    Assert.Equal("refreshToken", result.Value.RefreshToken);
    Assert.True(DateTime.UtcNow < result.Value.Expiration);
  }
}
