using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Whisper.Data.Repositories.Base;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.GroupRepository;
using Whisper.Data.Repositories.LocationRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;

namespace Whisper.Data;

public class DependencyContainerConfiguration(IServiceCollection services, IConfiguration configuration)
{
    public DependencyContainerConfiguration RegisterServices()
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        return this;
    }

    public DependencyContainerConfiguration RegisterDatabase(string dbConnectionStringKey)
    {
        var dbConnectionString = configuration.GetConnectionString(dbConnectionStringKey)
                               ?? throw new ArgumentNullException($"{dbConnectionStringKey} is not configured.");

        services.AddDbContext<WhisperDbContext>(options =>
            options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseNpgsql(dbConnectionString,
                    x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        return this;
    }

    public DependencyContainerConfiguration RegisterCacheStorage(string cacheStorageConnectionStringKey)
    {
        var cacheStorageconnectionString = configuration.GetConnectionString(cacheStorageConnectionStringKey)
            ?? throw new ArgumentNullException($"{cacheStorageConnectionStringKey} is not configured.");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheStorageconnectionString));
        services.AddSingleton<ICacheRepository, CacheRepository>();

        return this;
    }

    public DependencyContainerConfiguration SetNpgsqlContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return this;
    }
}