using MyWebApiDemo.Core.Models;

namespace MyWebApiDemo.Core.Services;
public class ProductService : IProductService
{
    // Use a static list as a data store for simplicity.
    private static readonly List<Product> Products = new()
    {
        new Product
        {
            Id = 1,
            Name = "Product 1",
            Description = "Description 1",
            UnitPrice = 10,
            Inventory = 10,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = DateTimeOffset.UtcNow
        },
        new Product
        {
            Id = 2,
            Name = "Product 2",
            Description = "Description 2",
            UnitPrice = 20,
            Inventory = 20,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = DateTimeOffset.UtcNow
        },
        new Product
        {
            Id = 3,
            Name = "Product 3",
            Description = "Description 3",
            UnitPrice = 30,
            Inventory = 30,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = DateTimeOffset.UtcNow
        },
        new Product
        {
            Id = 4,
            Name = "Product 4",
            Description = "Description 4",
            UnitPrice = 40,
            Inventory = 40,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = DateTimeOffset.UtcNow
        },
        new Product
        {
            Id = 5,
            Name = "Product 5",
            Description = "Description 5",
            UnitPrice = 50,
            Inventory = 50,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = DateTimeOffset.UtcNow
        }
    };

    public Task<List<Product>> GetProductsAsync()
    {
        return Task.FromResult(Products);
    }

    public Task<Product?> GetProductAsync(int id)
    {
        return Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
    }

    public Task<Product> CreateProductAsync(Product product)
    {
        product.Id = Products.Count + 1;
        product.CreatedDate = DateTimeOffset.UtcNow;
        product.UpdatedDate = DateTimeOffset.UtcNow;

        Products.Add(product);

        return Task.FromResult(product);
    }

    public Task<Product> UpdateProductAsync(Product product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id) ??
                              throw new InvalidOperationException($"Product with id {product.Id} not found.");
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.Inventory = product.Inventory;
        existingProduct.UpdatedDate = DateTimeOffset.UtcNow;

        return Task.FromResult(existingProduct);
    }

    public Task DeleteProductAsync(int id)
    {
        var existingProduct = Products.FirstOrDefault(p => p.Id == id) ??
                              throw new InvalidOperationException($"Product with id {id} not found.");
        Products.Remove(existingProduct);

        return Task.CompletedTask;
    }
}
