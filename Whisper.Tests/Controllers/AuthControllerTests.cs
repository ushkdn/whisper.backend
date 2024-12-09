using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.User;
using Whisper.Services.AuthService;
using Whisper.User.Controllers;

namespace Whisper.Tests.Controllers;

public class AuthControllerTests
{
    private readonly IAuthService _authService;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authService = A.Fake<IAuthService>();
        _authController = new AuthController(_authService);
    }

    [Fact]
    public async Task AuthController_Register_ReturnOk()
    {
        //Arrange
        var user = A.Fake<UserRegisterDto>();
        A.CallTo(() => _authService.Register(user)).Returns(Task.CompletedTask);

        //Act
        var result = await _authController.Register(user);

        //Assert
        var statusCodeResult = result as ObjectResult;
        statusCodeResult.Should().BeOfType<Task<IActionResult>>();
        statusCodeResult?.Value.Should().NotBeNull();
        statusCodeResult?.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task AuthController_ResetPassword_ReturnOk()
    {
        //Arrange
        var user = A.Fake<UserResetPasswordDto>();
        A.CallTo(() => _authService.ResetPassword(user)).Returns(Task.CompletedTask);

        //Act
        var result = await _authController.ResetPassword(user);

        //Assert
        var statusCodeResult = result as ObjectResult;
        statusCodeResult?.Value.Should().NotBeNull();
        statusCodeResult?.StatusCode.Should().Be(201);
    }
}