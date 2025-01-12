using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whisper.Data.Configurations.Base;
using Whisper.Data.Entities;

namespace Whisper.Data.Configurations;

internal sealed class UserEntityConfiguration : EntityBaseConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Surname).IsRequired().HasColumnName("surname");
        builder.Property(p => p.Name).IsRequired().HasColumnName("name");
        builder.Property(p => p.Username).HasMaxLength(15).HasColumnName("username");
        builder.Property(p => p.PhoneNumber).HasMaxLength(11).HasColumnName("phone_number");
        builder.Property(p => p.Email).IsRequired().HasColumnName("email");
        builder.Property(p => p.Password).IsRequired().HasMaxLength(120).HasColumnName("password");
        builder.Property(p => p.BirthDay).IsRequired().HasColumnName("birthday");
        builder.Property(p => p.IsVerified).HasColumnName("is_verified");

        builder.HasIndex(i => i.Email).IsUnique();
        builder.HasIndex(i => i.PhoneNumber).IsUnique();
        builder.HasIndex(i => i.Username).IsUnique();

        builder.HasOne(p => p.Location).WithMany(p => p.User).HasForeignKey(p => p.Id).HasConstraintName("location_id");
        builder.HasOne(p => p.RefreshToken).WithOne(p => p.User).HasForeignKey<RefreshTokenEntity>(p => p.Id).HasConstraintName("refresh_token_id");

        builder.ToTable(Tables.USERS);

    }
}