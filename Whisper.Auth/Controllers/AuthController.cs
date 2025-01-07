using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.User;
using Whisper.Data.Extensions;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Utils;
using Whisper.Data.Validations.DtosValidations.UserDtosValidations;
using Whisper.Services.AuthService;

namespace Whisper.User.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
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
    public async Task<IActionResult> Register(IValidator<UserRegisterDto> validator, [FromBody] UserRegisterDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                serviceResponse.Data = Results.ValidationProblem(validationResult.ToDictionary());
            }
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
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.ForgotPassword(email);

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

    [HttpPost("/{userId:guid}/reset-password")]
    public async Task<IActionResult> ResetPassword([FromRoute] Guid userId, [FromBody] UserResetPasswordDto user)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            await authService.ResetPassword(userId, user.Password, user.SecretCode);

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
            await authService.LogIn(user.Email, user.Password);

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

    [HttpPost("/{userId:guid}/verify")]
    public async Task<IActionResult> Verify([FromRoute] Guid userId, [FromBody] string secretCode)
    {
        var serviceResponse = new ServiceResponse<TokensModel>();

        try
        {
            var tokens = await authService.Verify(userId, secretCode);

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