using Microsoft.AspNetCore.Mvc;
using Whisper.Data.Dtos.User;
using Whisper.Services.UserService;

namespace Whisper.User.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService, IConfiguration configuration) : Controller
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
        var serviceResponse = await userService.Register(user);
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
        var serviceResponse = await userService.ForgotPassword(user);
        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }

    [HttpPost("/reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto user)
    {
        var serviceResponse = await userService.ResetPassword(user);
        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }

    [HttpPost("/log-in")]
    public async Task<IActionResult> LogIn([FromBody] UserLogInDto user)
    {
        var serviceResponse = await userService.LogIn(user);
        return StatusCode(serviceResponse.StatusCode, serviceResponse.Message);
    }
}