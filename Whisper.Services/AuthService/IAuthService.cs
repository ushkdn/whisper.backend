using Whisper.Data.Models;

namespace Whisper.Services.AuthService;

public interface IAuthService
{
    Task Register(UserModel user);

    Task<AuthTokensModel> LogIn(string email, string password);

    Task ForgotPassword(string email);

    Task ResetPassword(Guid userId, string password, string secretCode);

    Task<AuthTokensModel> Verify(Guid userId, string secretCode);
}