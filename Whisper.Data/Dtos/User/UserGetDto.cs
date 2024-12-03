namespace Whisper.Data.Dtos.User;

public record UserGetDto
{
    public string Surname { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime BirthDay { get; init; }
}