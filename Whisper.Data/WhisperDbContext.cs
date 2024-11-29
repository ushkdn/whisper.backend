using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Whisper.Data.Entities;

namespace Whisper.Data;


public class WhisperDbContextFactory : IDesignTimeDbContextFactory<WhisperDbContext>
{
    public WhisperDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Whisper.User");

        if (!Directory.Exists(basePath))
        {
            throw new DirectoryNotFoundException($"Base path not found: {basePath}");
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("Postgres");

        var optionsBuilder = new DbContextOptionsBuilder<WhisperDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new WhisperDbContext(optionsBuilder.Options);
    }
}
public class WhisperDbContext(DbContextOptions options) : DbContext(options)
{
    internal DbSet<UserEntity> Users { get; set; }
    internal DbSet<GroupEntity> Groups { get; set; }
    internal DbSet<LocationEntity> Locations { get; set; }
}