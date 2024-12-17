namespace Whisper.Data.Dtos.User;

public record UserGetDto
(
    string Surname,
    string Name,
    string Email,
    DateTime BirthDay
);