using Whisper.Data.Models;

namespace Whisper.Services.AuthService;

public interface IAuthService
{
    Task Register(UserModel user);

    Task LogIn(UserModel user);

    Task ForgotPassword(UserModel user);

    Task ResetPassword(UserModel user);

    Task Verify(UserModel user);
}