using Whisper.Data.Models;

namespace Whisper.Services.AuthService;

public interface IAuthService
{
    Task Register(UserModel user);

    Task<TokensModel> LogIn(UserModel user);

    Task ForgotPassword(UserModel user);

    Task ResetPassword(UserModel user);

    Task<TokensModel> Verify(UserModel user);
}