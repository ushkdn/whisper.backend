using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities;

namespace Whisper.Data.Configurations;

internal sealed class RefreshTokenEntityConfiguration : EntityBaseConfiguration<RefreshTokenEntity>, IEntityTypeConfiguration<RefreshTokenEntity>
{
    public override void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ExpireDate).IsRequired().HasColumnName("expire_date");
        builder.Property(p => p.Token).IsRequired().HasColumnName("token");


        builder.ToTable(Tables.REFRESH_TOKENS);

    }
}