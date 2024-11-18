using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities;

namespace Whisper.Data;

internal class WhisperDbContext(DbContextOptions options) : DbContext(options)
{
    internal DbSet<UserEntity> Users { get; set; }
    internal DbSet<GroupEntity> Groups { get; set; }
    internal DbSet<LocationEntity> Locations { get; set; }
}