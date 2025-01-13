using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities.UserGroup;

namespace Whisper.Data.Configurations.UserGroup;

internal sealed class UserModeratorGroupsEntityConfiguration : EntityConfiguration<UserModeratorGroupsEntity>
{
    public override void Configure(EntityTypeBuilder<UserModeratorGroupsEntity> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.UserId)
            .HasColumnName("user_id");

        builder
            .Property(p => p.GroupId)
            .HasColumnName("group_id");

        builder
            .HasOne(p => p.User)
            .WithMany(p => p.ModeratedGroups)
            .HasForeignKey(p => p.UserId);

        builder
            .HasOne(p => p.Group)
            .WithMany(p => p.Moderators)
            .HasForeignKey(p => p.GroupId);

        builder
            .ToTable(Tables.USER_MODERATOR_GROUPS);
    }
}