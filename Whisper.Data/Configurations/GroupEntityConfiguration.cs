using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities;

namespace Whisper.Data.Configurations;

internal sealed class GroupEntityConfiguration : EntityBaseConfiguration<GroupEntity>, IEntityTypeConfiguration<GroupEntity>
{
    public override void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.IsClosed).IsRequired().HasColumnName("is_closed");
        builder.Property(p => p.Title).IsRequired().HasColumnName("title");
        builder.Property(p => p.Description).HasColumnName("description");

        builder.HasMany(p => p.Followers).WithMany()

        builder.ToTable(Tables.GROUPS);
    }
}