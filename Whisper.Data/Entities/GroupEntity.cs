using Whisper.Data.Entities.Base;
using Whisper.Data.Entities.UserGroup;

namespace Whisper.Data.Entities;

public class GroupEntity : EntityBase
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool IsClosed { get; set; }

    public virtual List<UserFollowerGroupsEntity>? Followers { get; set; } = [];
    public virtual List<UserModeratorGroupsEntity>? Moderators { get; set; } = [];
    public virtual UserEntity? Owner { get; set; }
}