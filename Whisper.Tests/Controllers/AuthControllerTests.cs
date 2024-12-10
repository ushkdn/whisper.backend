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
    public async Task Register_ReturnOk()
    {
        //Arrange
        var user = A.Fake<UserRegisterDto>();
        A.CallTo(() => authService.Register(user)).Returns(Task.CompletedTask);

        //Act
        var result = await authController.Register(user);

        //Assert
        ObjectResult? statusCodeResult = result as ObjectResult;

        statusCodeResult.Should().NotBeNull().And.BeOfType<ObjectResult>();
        statusCodeResult?.StatusCode.Should().NotBeNull().And.Be(201);
        statusCodeResult?.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(typeof(KeyNotFoundException), 404)]
    [InlineData(typeof(InvalidOperationException), 400)]
    [InlineData(typeof(ArgumentNullException), 400)]
    [InlineData(typeof(ArgumentException), 400)]
    [InlineData(typeof(HttpRequestException), 400)]
    [InlineData(typeof(Exception), 500)]
    public async Task Register_ReturnBadRequest(Type exceptionType, int expectedStatusCode)
    {
        //Arrange
        var user = A.Fake<UserRegisterDto>();

        var exception = (Exception?)Activator.CreateInstance(exceptionType);
        A.CallTo(() => authService.Register(user))
            .ThrowsAsync(exception);

        //Act
        var result = await authController.Register(user);

        //Assert
        ObjectResult? statusCodeResult = result as ObjectResult;

        statusCodeResult.Should().NotBeNull().And.BeOfType<ObjectResult>();
        statusCodeResult?.Value.Should().NotBeNull();
        statusCodeResult?.StatusCode.Should().Be(expectedStatusCode);
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