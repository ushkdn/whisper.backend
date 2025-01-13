using Whisper.Data.Entities.Base;
using Whisper.Data.Entities.UserGroup;

namespace Whisper.Data.Entities;

public class UserEntity : EntityBase
{
    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime BirthDay { get; set; }

    public bool IsVerified { get; set; } = false;

    public virtual LocationEntity? Location { get; set; }

    public virtual RefreshTokenEntity? RefreshToken { get; set; }
    public virtual List<UserFollowerGroupsEntity>? FollowedGroups { get; set; } = [];
    public virtual List<UserModeratorGroupsEntity>? ModeratedGroups { get; set; } = [];
    public virtual List<GroupEntity>? OwnedGroups { get; set; } = [];
}