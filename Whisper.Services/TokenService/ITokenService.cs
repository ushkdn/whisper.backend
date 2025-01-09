using Whisper.Data.Models;

namespace Whisper.Services.TokenService;

public interface ITokenService
{
    Task<AuthTokensModel> RefreshTokens();

    AuthTokensModel CreateTokensAndSetRefreshToken(UserModel user);
}