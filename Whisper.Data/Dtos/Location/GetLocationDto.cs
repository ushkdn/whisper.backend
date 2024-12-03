using Whisper.Data.Entities.Base;

namespace Whisper.Data.Dtos.Location;

public record GetLocationDto : Entity
{
    public required string Country { get; init; }
}