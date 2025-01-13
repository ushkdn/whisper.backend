using Whisper.Data.Entities.Base;

namespace Whisper.Data.Entities;

public class GroupEntity : EntityBase
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool IsClosed { get; set; }

    public virtual List<UserGroupEntity>? Followers { get; set; } = [];
    //public virtual List<UserEntity>? Moderators { get; set; } = [];
    //public virtual UserEntity? Admin { get; set; }
}