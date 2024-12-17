using Whisper.Data.Dtos.Base;
using Whisper.Data.Dtos.Location;

namespace Whisper.Data.Dtos.User;

public record class GetUserDto
(
    Guid Id,
    string Surname,
    string Name,
    string Username,
    DateTime Birthday,
    GetLocationDto Location,
    DateTime DateCreated,
    DateTime DateUpdated
) : DtoBase(Id, DateCreated, DateUpdated);