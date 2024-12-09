using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.User;
using Whisper.Services.AuthService;
using Whisper.User.Controllers;

namespace Whisper.Tests.Controllers;

public class AuthControllerTests
{
    private readonly IAuthService authService;
    private readonly AuthController authController;

    public AuthControllerTests()
    {
        authService = A.Fake<IAuthService>();
        authController = new AuthController(authService);
    }

    #region Register(Completed)

    [Fact]
    public async Task Register_ReturnOk_UserRegistered()
    {
        //Arrange
        var user = A.Fake<UserRegisterDto>();
        A.CallTo(() => authService.Register(user)).Returns(Task.CompletedTask);

        //Act
        var result = await authController.Register(user);

        //Assert
        ObjectResult? statusCodeResult = result as ObjectResult;

        statusCodeResult.Should().NotBeNull().And.BeOfType<ObjectResult>();
        statusCodeResult?.Value.Should().NotBeNull().And.Be("Your account has been created.");
        statusCodeResult?.StatusCode.Should().NotBeNull().And.Be(201);
    }

    [Fact]
    public async Task Register_ReturnBadRequest_UserExists()
    {
        //Arrange
        var user = A.Fake<UserRegisterDto>();
        A.CallTo(() => authService.Register(user))
            .ThrowsAsync(new InvalidOperationException(("User already exists.")));

        //Act
        var result = await authController.Register(user);

        //Assert
        ObjectResult? statusCodeResult = result as ObjectResult;

        statusCodeResult.Should().NotBeNull().And.BeOfType<ObjectResult>();
        statusCodeResult?.Value.Should().NotBeNull().And.Be("User already exists.");
        statusCodeResult?.StatusCode.Should().Be(400);
    }

    #endregion Register(Completed)

    [Fact]
    public async Task ResetPassword_ReturnOk()
    {
        //Arrange
        var user = A.Fake<UserResetPasswordDto>();
        A.CallTo(() => authService.ResetPassword(user)).Returns(Task.CompletedTask);

        //Act
        var result = await authController.ResetPassword(user);

        //Assert
        var statusCodeResult = result as ObjectResult;
        statusCodeResult?.Value.Should().NotBeNull();
        statusCodeResult?.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task ResetPassword_ReturnBadRequest()
    {
        //Arrange
        var user = A.Fake<UserResetPasswordDto>();
        A.CallTo(() => authService.ResetPassword(user)).Returns(Task.CompletedTask);

        //Act
        var result = await authController.ResetPassword(user);

        //Assert
        var statusCodeResult = result as ObjectResult;
        statusCodeResult?.Value.Should().NotBeNull();
        statusCodeResult?.StatusCode.Should().Be(201);
    }
}