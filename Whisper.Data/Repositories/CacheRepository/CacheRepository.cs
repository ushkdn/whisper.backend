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

        return storedList.Where(x => x.Id == id).FirstOrDefault()
            ?? throw new KeyNotFoundException($"Unable to find entry with key:{key} and id:{id}");
    }

    public async Task<List<TTable>> GetListAsync<TTable>(string key) where TTable : class, IEntity
    {
        var listValue = await cache.ListRangeAsync(key);
        var storedList = listValue.Select(value => JsonConvert.DeserializeObject<TTable>(value)).ToList()
            ?? throw new InvalidOperationException($"Unable to parse data with key:{key} from cache storage");

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
        throw new KeyNotFoundException($"Unable to find entry with key:{key} and id:{id} to delete from cache storage");
    }

    public async Task RemoveAllAsync(string key)
    {
        await cache.KeyDeleteAsync(key);
    }

    public async Task SetSingleAsync<TTable>(string key, TTable value, DateTimeOffset? expirationDate = null) where TTable : class, IEntity
    {
        if (expirationDate != null)
        {
            var expiryTime = expirationDate.Value.DateTime.Subtract(DateTime.UtcNow);
            await cache.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);
            return;
        }
        await cache.StringSetAsync(key, JsonConvert.SerializeObject(value));
    }

    public async Task<TTable> GetSingleAsync<TTable>(string key) where TTable : class, IEntity
    {
        var value = await cache.StringGetAsync(key);
        if (!String.IsNullOrEmpty(value))
        {
            var parsedValue = JsonConvert.DeserializeObject<TTable>(value)
                ?? throw new InvalidOperationException($"$Unable to parse data with key:{key} from cache storage");
        }
        throw new KeyNotFoundException($"Unable to find entry with key:{key} from cache storage");
    }
}