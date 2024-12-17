namespace Whisper.Data.Dtos.User;

public record UserUpdateDto
(
    string Surname,
    string Name,
    string Username,
    string PhoneNumber,
    string Email,
    string Password,
    string ConfirmPassword,
    DateTime BirthDay
);