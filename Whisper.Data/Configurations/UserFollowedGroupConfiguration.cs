using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities;

namespace Whisper.Data.Configurations;

internal sealed class UserFollowedGroupConfiguration : EntityConfiguration<UserGroupEntity>
{
    public override void Configure(EntityTypeBuilder<UserGroupEntity> builder)
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
            .WithMany(p => p.FollowedGroups)
            .HasForeignKey(p => p.UserId);

        builder
            .HasOne(p => p.Group)
            .WithMany(p => p.Followers)
            .HasForeignKey(p => p.GroupId);

        builder
            .ToTable(Tables.USER_FOLLOWED_GROUPS);
    }
}