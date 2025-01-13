using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities.UserGroup;

public abstract class UserGroupBase : Entity
{
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual GroupEntity? Group { get; set; }
}