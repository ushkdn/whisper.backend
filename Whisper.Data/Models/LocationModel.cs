using Whisper.Data.Entities;

namespace Whisper.Data.Models;

public record LocationModel
{
    public string? Country { get; set; }

    public virtual List<UserEntity>? User { get; set; }
}