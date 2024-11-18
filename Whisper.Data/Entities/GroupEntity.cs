using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public record GroupEntity : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<UserEntity> Followers { get; set; } = new();
    public bool IsClosed { get; set; }
}