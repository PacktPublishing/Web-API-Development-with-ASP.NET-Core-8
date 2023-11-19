using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7213");
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        return result ?? new List<Product>();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
        return result;
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        var result = await _httpClient.PostAsJsonAsync("api/products", product);
        return await result.Content.ReadFromJsonAsync<Product>();
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
        return await result.Content.ReadFromJsonAsync<Product>();
    }

    public async Task DeleteProductAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/products/{id}");
    }
}
