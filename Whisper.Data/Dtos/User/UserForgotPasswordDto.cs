namespace Whisper.Data.Dtos.User;

public record UserForgotPasswordDto
{
    public string EmailOrPhoneNumber { get; init; } = string.Empty;
}