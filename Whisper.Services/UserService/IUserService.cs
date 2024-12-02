using Whisper.Data.Dtos.User;
using Whisper.Data.Utils;

namespace Whisper.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<string>> Register(UserRegisterDto request);

    Task<ServiceResponse<string>> LogIn(UserLogInDto request);

    Task<ServiceResponse<string>> ForgotPassword(UserForgotPasswordDto request);

    Task<ServiceResponse<string>> ResetPassword(UserResetPasswordDto request);
}