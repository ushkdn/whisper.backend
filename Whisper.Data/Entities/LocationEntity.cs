using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public class LocationEntity : Entity
{
    public string? Country { get; set; }

    public virtual List<UserEntity>? User { get; set; } = [];
}