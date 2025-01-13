using Whisper.Data.Models;
using Whisper.Data.Utils;

namespace Whisper.Services.AuthService;

public interface IAuthService
{
    Task Register(UserModel user);

    Task<AuthTokens> LogIn(string email, string password);

    Task ForgotPassword(string email);

    Task ResetPassword(Guid userId, string password, string secretCode);

    Task<AuthTokens> Verify(Guid userId, string secretCode);
}