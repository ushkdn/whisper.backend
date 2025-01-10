using Whisper.Data.Dtos.Location;

namespace Whisper.Data.Dtos.User;

public record UserRegisterDto
(
    string Surname,
    string Name,
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    DateTime Birthday,
    AddLocationDto Location
);