namespace Whisper.Data.Dtos.User
{
    public record UserRegisterDto
    {
        public string Surname { get; init; } = string.Empty;
        public string Name {get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public DateTime Birthday { get; init; }
    }
}