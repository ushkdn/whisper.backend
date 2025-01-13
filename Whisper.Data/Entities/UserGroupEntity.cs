using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public class UserGroupEntity : Entity
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual GroupEntity? Group { get; set; }
}