namespace Whisper.Data.Dtos.User
{
    public record UserResetPasswordDto
    {
        public string EmailOrPhoneCode { get; init; } = string.Empty;
        public string NewPassword { get; init; } = string.Empty;
    }
}