using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public record UserEntity : EntityBase
{
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDay { get; init; }
    public LocationEntity? Location { get; set; }
    public List<GroupEntity> Groups { get; set; } = new();
}