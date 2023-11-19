using CachingDemo.Models;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CachingDemo.Services;

public class CategoryService : ICategoryService
{
    // Use a static list to simulate a database

    private static readonly List<Category> Categories = new()
    {
        new Category { Id = 1, Name = "Toys", Description = "Soft toys, action figures, dolls, and puzzles" },
        new Category { Id = 2, Name = "Electronics", Description = "Smartphones, tablets, laptops, and smartwatches" },
        new Category { Id = 3, Name = "Clothing", Description = "Shirts, pants, dresses, and shoes" },
        new Category { Id = 4, Name = "Books", Description = "Fiction, non-fiction, and textbooks" },
        new Category { Id = 5, Name = "Home", Description = "Furniture, appliances, and decor" },
        new Category { Id = 6, Name = "Beauty", Description = "Makeup, skincare, and haircare" },
        new Category { Id = 7, Name = "Sports", Description = "Sporting goods and equipment" },
        new Category { Id = 8, Name = "Food", Description = "Snacks, drinks, and ingredients" }
    };

    private static readonly Dictionary<int, List<Category>> FavoritesCategories = new()
    {
        { 1, new List<Category> { Categories[0], Categories[1], Categories[2] } },
        { 2, new List<Category> { Categories[3], Categories[4], Categories[5] } },
        { 3, new List<Category> { Categories[6], Categories[7] } }
    };

    private readonly ILogger<CategoryService> _logger;
    private readonly IMemoryCache _cache;
    private readonly IDistributedCache _distributedCache;

    public CategoryService(ILogger<CategoryService> logger, IMemoryCache cache, IDistributedCache distributedCache)
    {
        _logger = logger;
        _cache = cache;
        _distributedCache = distributedCache;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        // Try to get the categories from the cache
        if (_cache.TryGetValue(CacheKeys.Categories, out IEnumerable<Category>? categories))
        {
            _logger.LogInformation("Getting categories from cache");
            return categories ?? new List<Category>();
        }

        // Simulate a database query
        _logger.LogInformation("Getting categories from the database");
        await Task.Delay(2000);
        await RefreshCategoriesCache();
        return Categories;
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        // The following code is not the best way to use the cache because it still queries the database even if the item is not found in the database
        //if (_cache.TryGetValue($"{CacheKeys.Categories}:{id}", out Category? category))
        //{
        //    _logger.LogInformation($"Getting category with id {id} from cache");
        //    return category;
        //}
        //// Simulate a database query
        //_logger.LogInformation($"Getting category with id {id} from the database");
        //await Task.Delay(2000);
        //var result = Categories.FirstOrDefault(c => c.Id == id);
        //if (result is not null)
        //{
        //    _cache.Set($"{CacheKeys.Categories}:{id}", result);
        //}

        // The following code will set the cache as null if the item is not found in the database.
        // So the next time the item is requested, it will not query the database again
        var category = await _cache.GetOrCreateAsync($"{CacheKeys.Categories}:{id}", async entry =>
        {
            // Simulate a database query
            _logger.LogInformation($"Getting category with id {id} from the database");
            await Task.Delay(2000);
            return Categories.FirstOrDefault(c => c.Id == id);
        });
        return category;
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        category.Id = Categories.Max(c => c.Id) + 1;
        Categories.Add(category);
        await RefreshCategoriesCache();
        return category;
    }

    private async Task RefreshCategoriesCache()
    {
        // Query the database first
        _logger.LogInformation("Getting categories from the database");
        await Task.Delay(2000);
        var categories = Categories;
        // Then refresh the cache
        _cache.Remove(CacheKeys.Categories);
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(10),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
        _cache.Set(CacheKeys.Categories, categories, cacheEntryOptions);
    }

    public async Task<Category?> UpdateCategoryAsync(Category category)
    {
        var existingCategory = Categories.FirstOrDefault(c => c.Id == category.Id);
        if (existingCategory == null)
        {
            return null;
        }

        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        await RefreshCategoriesCache();
        return existingCategory;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var existingCategory = Categories.FirstOrDefault(c => c.Id == id);
        if (existingCategory == null)
        {
            return false;
        }

        Categories.Remove(existingCategory);
        await RefreshCategoriesCache();
        return true;
    }

    public async Task<IEnumerable<Category>?> GetFavoritesCategoriesAsync(int userId)
    {
        // Try to get the categories from the distributed cache
        var cacheKey = $"{CacheKeys.FavoritesCategories}:{userId}";

        // Use the GetAsync method to get the bytes from the distributed cache. Need to convert the bytes to string.
        //var bytes = await _distributedCache.GetAsync(cacheKey);
        //if (bytes is { Length: > 0 })
        //{
        //    _logger.LogInformation("Getting favorites categories from distributed cache");
        //    var serializedFavoritesCategories = Encoding.UTF8.GetString(bytes);
        //    var favoritesCategories = JsonSerializer.Deserialize<IEnumerable<Category>>(serializedFavoritesCategories);
        //    return favoritesCategories ?? new List<Category>();
        //}

        // Use the GetStringAsync method to get the result from the distributed cache
        //var json = await _distributedCache.GetStringAsync(cacheKey);
        //if (!string.IsNullOrEmpty(json))
        //{
        //_logger.LogInformation("Getting favorites categories from distributed cache");
        //var favoritesCategories = JsonSerializer.Deserialize<IEnumerable<Category>>(json);
        //return favoritesCategories ?? new List<Category>();
        //}

        // Simulate a database query
        //_logger.LogInformation("Getting favorites categories from the database");
        //var categories = FavoritesCategories[userId];
        //await Task.Delay(2000);
        //// Store the result in the distributed cache
        //var cacheEntryOptions = new DistributedCacheEntryOptions
        //{
        //    SlidingExpiration = TimeSpan.FromMinutes(10),
        //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        //};
        //var serializedCategories = JsonSerializer.Serialize(categories);
        //var serializedCategoriesBytes = Encoding.UTF8.GetBytes(serializedCategories);
        //await _distributedCache.SetAsync(cacheKey, serializedCategoriesBytes, cacheEntryOptions);

        // Use the SetStringAsync method to store the result in the distributed cache
        //await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(categories), cacheEntryOptions);

        // Use the GetOrCreateAsync extension method to get the result from the distributed cache
        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(10),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
        var favoritesCategories = await _distributedCache.GetOrCreateAsync(cacheKey, async () =>
        {
            // Simulate a database query
            _logger.LogInformation("Getting favorites categories from the database");
            var categories = FavoritesCategories[userId];
            await Task.Delay(2000);
            return categories;
        }, cacheEntryOptions);
        return favoritesCategories?.AsEnumerable();
    }
}
