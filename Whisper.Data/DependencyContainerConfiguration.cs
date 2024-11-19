using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whisper.Data.Repositories.Base;
using Whisper.Data.Repositories.Group;
using Whisper.Data.Repositories.Location;
using Whisper.Data.Repositories.User;

namespace Whisper.Data;

public class DependencyContainerConfiguration
{
    public DependencyContainerConfiguration RegisterServices(
        IServiceCollection services,
        IConfiguration configuration,
        string connectionStringKey)
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey)
            ?? throw new ArgumentNullException($"{connectionStringKey} is not configured.");

        services.AddDbContext<WhisperDbContext>(options =>
            options
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseNpgsql(connectionString,
                x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        return this;
    }

    public DependencyContainerConfiguration SetNpgsqlContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return this;
    }
}