using Whisper.Data.Dtos.Location;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Dtos.User;

public record GetUserDto : EntityBase
{
    public required string Surname { get; init; }

    public required string Name { get; init; }

    public required string Username { get; init; }

    public DateTime Birthday { get; init; }

    public required GetLocationDto Location { get; init; }
}