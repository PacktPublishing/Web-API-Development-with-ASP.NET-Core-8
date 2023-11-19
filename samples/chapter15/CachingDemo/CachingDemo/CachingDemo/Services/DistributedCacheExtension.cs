using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

namespace CachingDemo.Services;

public static class DistributedCacheExtension
{
    public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T?>> createAsync, DistributedCacheEntryOptions? options = null)
    {
        // Get the value from the cache.
        // If the value is found, return it.
        var value = await cache.GetStringAsync(key);
        if (!string.IsNullOrWhiteSpace(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        // If the value is not cached, then create it using the provided function.
        var result = await createAsync();
        var json = JsonSerializer.Serialize(result);
        await cache.SetStringAsync(key, json, options ?? new DistributedCacheEntryOptions());
        return result;
    }
}
