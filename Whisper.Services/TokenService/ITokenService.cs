using Whisper.Data.Models;
using Whisper.Data.Utils;

namespace Whisper.Services.TokenService;

public interface ITokenService
{
    Task<AuthTokens> RefreshTokens();

    AuthTokens CreateTokensAndSetRefreshToken(UserModel user);
}