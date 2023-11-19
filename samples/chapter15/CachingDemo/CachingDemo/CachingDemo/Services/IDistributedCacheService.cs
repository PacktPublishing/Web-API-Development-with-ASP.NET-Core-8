using Microsoft.Extensions.Caching.Distributed;

namespace CachingDemo.Services;

public interface IDistributedCacheService
{
    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T?>> createAsync, DistributedCacheEntryOptions? options = null);
}
