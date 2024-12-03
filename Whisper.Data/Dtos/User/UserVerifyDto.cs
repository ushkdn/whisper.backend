namespace Whisper.Data.Dtos.User;

public record UserVerifyDto
{
    public required string Email { get; init; }
    public required string SecretCode { get; init; }
}