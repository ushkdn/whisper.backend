using Whisper.Data.Models;

namespace Whisper.Services.TokenService;

public interface ITokenService
{
    Task<AuthTokensModel> RefreshTokens();

    void SetRefreshToken(RefreshTokenModel refreshToken);

    RefreshTokenModel CreateRefreshToken();

    string CreateAccessToken(UserModel userModel);

    AuthTokensModel CreateTokensAndSetRefreshToken(UserModel user);
}