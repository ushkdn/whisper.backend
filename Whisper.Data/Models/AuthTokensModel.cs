namespace Whisper.Data.Models;

public record AuthTokensModel
(
    string AccessToken,
    RefreshTokenModel RefreshToken
);