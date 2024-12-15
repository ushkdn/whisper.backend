using Newtonsoft.Json;
using StackExchange.Redis;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Repositories.CacheRepository;

public class CacheRepository : ICacheRepository
{
    private readonly IDatabase cache;

    public CacheRepository(IConnectionMultiplexer redis)
    {
        cache = redis.GetDatabase();
    }

    public async Task<TTable> GetSingleFromListAsync<TTable>(string key, Guid id) where TTable : class, IEntity
    {
        var storedList = await GetListAsync<TTable>(key);

        return storedList.Where(x => x.Id == id).FirstOrDefault();
    }

    public async Task<List<TTable>> GetListAsync<TTable>(string key) where TTable : class, IEntity
    {
        var listValue = await cache.ListRangeAsync(key);
        var storedList = listValue.Select(value => JsonConvert.DeserializeObject<TTable>(value)).ToList();

        return storedList;
    }

    public async Task SetListAsync<TTable>(string key, List<TTable> values) where TTable : class, IEntity
    {
        foreach (var value in values)
        {
            await cache.ListRightPushAsync(key, JsonConvert.SerializeObject(value));
        }
    }

    public async Task AddOrUpdateListAsync<TTable>(string key, TTable value) where TTable : class, IEntity
    {
        var cachedList = await GetListAsync<TTable>(key);

        int checkIndexForSameEntry = cachedList.FindIndex(x => x.Id == value.Id);

        if (checkIndexForSameEntry != -1)
        {
            await cache.ListSetByIndexAsync(key, checkIndexForSameEntry, JsonConvert.SerializeObject(value));
            return;
        }
        await cache.ListRightPushAsync(key, JsonConvert.SerializeObject(value));
    }

    public async Task RemoveByIdAsync<TTable>(string key, Guid id) where TTable : class, IEntity
    {
        var cachedList = await GetListAsync<TTable>(key);
        int elementEntryId = cachedList.FindIndex(x => x.Id == id);
        if (elementEntryId != -1)
        {
            var elementValue = await GetSingleFromListAsync<TTable>(key, id);
            await cache.ListRemoveAsync(key, JsonConvert.SerializeObject(elementValue));
        }
    }

    public async Task RemoveAllAsync(string key)
    {
        await cache.KeyDeleteAsync(key);
    }

    public async Task SetSingleAsync<T>(string key, T value, DateTimeOffset? expirationDate = null)
    {
        if (expirationDate != null)
        {
            var expiryTime = expirationDate.Value.DateTime.Subtract(DateTime.UtcNow);
            await cache.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);
            return;
        }
        await cache.StringSetAsync(key, JsonConvert.SerializeObject(value));
    }

    public async Task<T?> GetSingleAsync<T>(string key)
    {
        var value = await cache.StringGetAsync(key);
        if (!String.IsNullOrEmpty(value))
        {
            var parsedValue = JsonConvert.DeserializeObject<T>(value);
            return parsedValue;
        }
        //todo: return null
        return default;
    }
}