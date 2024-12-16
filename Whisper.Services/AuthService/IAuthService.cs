using Whisper.Data.Models;

namespace Whisper.Services.AuthService;

public interface IAuthService
{
    Task Register(UserModel user);

    Task<TokensModel> LogIn(string email, string password);

    Task ForgotPassword(string email);

    Task ResetPassword(Guid userId, string secretCode, string password);

    Task<TokensModel> Verify(Guid userId, string secretCode);
}