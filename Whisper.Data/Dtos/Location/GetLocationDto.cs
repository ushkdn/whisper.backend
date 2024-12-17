using Whisper.Data.Dtos.Base;

namespace Whisper.Data.Dtos.Location;

public record GetLocationDto(Guid Id, string Country) : Dto(Id);