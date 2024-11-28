namespace Whisper.Data.Dtos.User
{
    public record UserLoginDto
    {
        public string EmailOrPhoneNumber { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}