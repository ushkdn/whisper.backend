using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.User;
using Whisper.Data.Extensions;
using Whisper.Data.Utils;
using Whisper.Services.AuthService;

namespace Whisper.User.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    #region RegisterSwaggerDoc

    /// <summary>
    /// Register an User
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/users/register
    ///     {
    ///         "surname": "string",
    ///         "name": "string",
    ///         "username": "string",
    ///         "phoneNumber": "stringstrin",
    ///         "email": "user@example.com",
    ///         "password": "string",
    ///         "confirmPassword": "string",
    ///         "birthday": "2024-12-02T12:25:53.249Z",
    ///         "location": {
    ///                 "country": "string"
    ///         }
    ///     }
    /// </remarks>
    /// <returns>Returns a status code with data or message</returns>
    /// <response code = "200">Request sent successfully with received response body</response>
    /// <response code = "201">Request sent successfully without response body</response>
    /// <response code = "400">Invalid data specified</response>
    /// <response code = "404">Unable to find entity in database</response>
    /// <response code = "500">Internal server error</response>

    #endregion RegisterSwaggerDoc

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.Register(user);

            serviceResponse.StatusCode = 201;
            serviceResponse.Success = true;
            serviceResponse.Message = "Your account has been created.";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }

    #region LogInSwaggerDoc

    /// <summary>
    /// User log-in
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/users/log-in
    ///     {
    ///        "emailOrPhoneNumber": "string",
    ///        "password": "string"
    ///     }
    /// </remarks>
    /// <returns>Returns a status code with data or message</returns>
    /// <response code = "200">Request sent successfully with received response body</response>
    /// <response code = "201">Request sent successfully without response body</response>
    /// <response code = "400">Invalid data specified</response>
    /// <response code = "404">Unable to find entity in database</response>
    /// <response code = "500">Internal server error</response>

    #endregion LogInSwaggerDoc

    [HttpPost("/login")]
    public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.ForgotPassword(user);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Code for password reset sent to your email.";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }

    [HttpPost("/reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.ResetPassword(user);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "Password changed.";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }

    [HttpPost("/log-in")]
    public async Task<IActionResult> LogIn([FromBody] UserLogInDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.LogIn(user);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "You are logged in.";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }

    [HttpPost("/verify")]
    public async Task<IActionResult> Verify([FromBody] UserVerifyDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.Verify(user);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "Your account has been verified.";
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }
}