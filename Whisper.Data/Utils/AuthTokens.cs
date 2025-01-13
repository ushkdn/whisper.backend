using Whisper.Data.Models;

namespace Whisper.Data.Utils;

public record AuthTokens
(
    string AccessToken,
    RefreshTokenModel RefreshToken
);