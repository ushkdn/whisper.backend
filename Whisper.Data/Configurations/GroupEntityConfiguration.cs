using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities;

namespace Whisper.Data.Configurations;

internal sealed class GroupEntityConfiguration : EntityBaseConfiguration<GroupEntity>
{
    public override void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.IsClosed)
            .IsRequired()
            .HasColumnName("is_closed");

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("title");

        builder
            .Property(p => p.Description)
            .HasColumnName("description");

        builder
            .ToTable(Tables.GROUPS);
    }
}