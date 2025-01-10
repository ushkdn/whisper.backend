using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Whisper.Core.Helpers;
using Whisper.Data.Dtos.Tokens;
using Whisper.Data.Dtos.User;
using Whisper.Data.Extensions;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Utils;
using Whisper.Services.AuthService;

namespace Whisper.User.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    IValidator<UserRegisterDto> userRegisterDtoValidator,
    IValidator<UserResetPasswordDto> userResetPasswordDtoValidator,
    IValidator<UserLogInDto> userLogInDtoValidator,
    IAuthService authService
    ) : ControllerBase
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

        var validationResult = ValidationHelper.Validate(userRegisterDtoValidator, user);
        if (!validationResult.Success)
        {
            return StatusCode(validationResult.StatusCode, validationResult);
        }

        try
        {
            var userModel = WhisperMapper.Mapper.Map<UserModel>(user);

            await authService.Register(userModel);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "Your account has been created.";
            serviceResponse.Data = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse);
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
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.ForgotPassword(email);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Code for password reset sent to your email.";
            serviceResponse.Data = null;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse);
    }

    [HttpPost("/{userId:guid}/reset-password")]
    public async Task<IActionResult> ResetPassword([FromRoute] Guid userId, [FromBody] UserResetPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        var validationResult = ValidationHelper.Validate(userResetPasswordDtoValidator, user);
        if (!validationResult.Success)
        {
            return StatusCode(validationResult.StatusCode, validationResult);
        }

        try
        {
            await authService.ResetPassword(userId, user.Password, user.SecretCode);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "Password changed.";
            serviceResponse.Data = null;
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<string>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse);
    }

    [HttpPost("log-in")]
    public async Task<IActionResult> LogIn([FromBody] UserLogInDto user)
    {
        var serviceResponse = new ServiceResponse<GetAuthTokensDto>();

        var validationResult = ValidationHelper.Validate(userLogInDtoValidator, user);
        if (!validationResult.Success)
        {
            return StatusCode(validationResult.StatusCode, validationResult);
        }

        try
        {
            var authTokens = await authService.LogIn(user.Email, user.Password);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 201;
            serviceResponse.Message = "You are logged in.";
            serviceResponse.Data = new GetAuthTokensDto(authTokens.AccessToken, authTokens.RefreshToken.Token);
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<GetAuthTokensDto>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse);
    }

    [HttpPost("/{userId:guid}/verify")]
    public async Task<IActionResult> Verify([FromRoute] Guid userId, [FromBody] string secretCode)
    {
        var serviceResponse = new ServiceResponse<GetAuthTokensDto>();

        try
        {
            var authTokens = await authService.Verify(userId, secretCode);

            serviceResponse.Success = true;
            serviceResponse.StatusCode = 200;
            serviceResponse.Message = "Your account has been verified.";
            serviceResponse.Data = new GetAuthTokensDto(authTokens.AccessToken, authTokens.RefreshToken.Token);
        }
        catch (Exception ex)
        {
            serviceResponse = ex.ToServiceResponse<GetAuthTokensDto>();
        }

        return StatusCode(serviceResponse.StatusCode, serviceResponse);
    }
}