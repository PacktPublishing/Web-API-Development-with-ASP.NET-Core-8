using Microsoft.Extensions.Caching.Memory;

namespace CachingDemo;

public class CategoriesCacheBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CategoriesCacheBackgroundService> _logger;
    private readonly IMemoryCache _cache;

    public CategoriesCacheBackgroundService(IServiceProvider serviceProvider, ILogger<CategoriesCacheBackgroundService> logger, IMemoryCache cache)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _cache = cache;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Remove the cache every 1 hour
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Updating the cache in background service");

            // Do something to query the database.
            // The following code is commented out because there is another method to update the cache in CategoryService.cs
            //using var scope = _serviceProvider.CreateScope();
            //var categoryService = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            //var categories = await categoryService.GetCategoriesAsync();
            //_cache.Remove(CacheKeys.Categories);
            //_cache.Set(CacheKeys.Categories, categories, TimeSpan.FromHours(1));
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
