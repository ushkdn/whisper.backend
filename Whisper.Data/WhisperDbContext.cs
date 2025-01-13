﻿using Microsoft.EntityFrameworkCore;
using Whisper.Data.Configurations;
using Whisper.Data.Entities;
using Whisper.Data.Entities.UserGroup;

namespace Whisper.Data;

public class WhisperDbContext(DbContextOptions options) : DbContext(options)
{
    internal DbSet<UserEntity> Users { get; set; }
    internal DbSet<GroupEntity> Groups { get; set; }
    internal DbSet<LocationEntity> Locations { get; set; }
    internal DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    internal DbSet<UserFollowerGroupsEntity> UserFollowedGroups { get; set; }
    internal DbSet<UserModeratorGroupsEntity> UserModeratorGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserFollowerGroupsConfiguration());
        modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserModeratorGroupsConfiguration());
    }
}