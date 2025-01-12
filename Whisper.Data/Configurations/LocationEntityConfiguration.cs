using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities;

namespace Whisper.Data.Configurations;

internal sealed class LocationEntityConfiguration : EntityConfiguration<LocationEntity>, IEntityTypeConfiguration<LocationEntity>
{
    public override void Configure(EntityTypeBuilder<LocationEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Country).IsRequired().HasMaxLength(25).HasColumnName("country");

        builder.HasIndex(i => i.Country).IsUnique();

        builder.HasMany(p => p.User).WithOne(p => p.Location).HasForeignKey(p => p.Id);

        builder.ToTable(Tables.LOCATIONS);

    }
}