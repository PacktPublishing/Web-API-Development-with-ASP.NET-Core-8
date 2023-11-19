using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Core.Services;
public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}
