namespace Whisper.Data.Models;

public record TokensModel
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}