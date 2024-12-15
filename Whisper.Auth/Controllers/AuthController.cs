using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Whisper.Data.Dtos.User;
using Whisper.Data.Extensions;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Utils;
using Whisper.Services.AuthService;
using Whisper.Services.TokenService;

namespace Whisper.User.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService, ITokenService tokenService) : ControllerBase
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            var userModel = WhisperMapper.Mapper.Map<UserModel>(user);
            await authService.Register(userModel);

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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            var userModel = WhisperMapper.Mapper.Map<UserModel>(user);
            await authService.ForgotPassword(userModel);

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

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            var userModel = WhisperMapper.Mapper.Map<UserModel>(user);
            await authService.ResetPassword(userModel);

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

    [HttpPost("log-in")]
    public async Task<IActionResult> LogIn([FromBody] UserLogInDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            var userModel = WhisperMapper.Mapper.Map<UserModel>(user);
            await authService.LogIn(userModel);

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

    [HttpPost("verify")]
    public async Task<IActionResult> Verify([FromBody] UserVerifyDto user)
    {
        var serviceResponse = new ServiceResponse<TokensModel>();

        try
        {
            var userModel = WhisperMapper.Mapper.Map<UserModel>(user);
            var tokens = await authService.Verify(userModel);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Your account has been verified.";
            serviceResponse.Data = tokens;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<TokensModel>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Data);
    }

    [HttpPost("refresh-tokens")]
    public async Task<IActionResult> RefreshTokens()
    {
        var serviceResponse = new ServiceResponse<TokensModel>();

        try
        {
            var tokens = await tokenService.RefreshTokens();

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Your account has been verified.";
            serviceResponse.Data = tokens;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<TokensModel>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse.Data);
    }
}