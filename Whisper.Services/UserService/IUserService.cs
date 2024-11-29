using Whisper.Data.Dtos.User;
using Whisper.Data.Utils;

namespace Whisper.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<string>> Register(UserRegisterDto user);

    Task<ServiceResponse<string>> LogIn(UserLogInDto user);

    Task<ServiceResponse<string>> ForgotPassword(UserForgotPasswordDto user);

    Task<ServiceResponse<string>> ResetPassword(UserResetPasswordDto user);
}