using Whisper.Data.Models;

namespace Whisper.Services.TokenService;

public interface ITokenService
{
    Task<TokensModel> RefreshTokens();

    RefreshTokenModel CreateRefreshToken();

    void SetRefreshToken(RefreshTokenModel refreshToken);

    string CreateAccessToken(UserModel userModel);

    TokensModel CreateTokensAndSetRefreshToken(UserModel user);
}