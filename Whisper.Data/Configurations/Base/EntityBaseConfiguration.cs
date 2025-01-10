using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Configurations.Base;

internal class EntityBaseConfiguration<TEntity> : EntityConfiguration<TEntity>, IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.DateCreated).IsRequired().HasColumnName("date_created");
        builder.Property(x => x.DateUpdated).IsRequired().HasColumnName("date_updated");
    }
}