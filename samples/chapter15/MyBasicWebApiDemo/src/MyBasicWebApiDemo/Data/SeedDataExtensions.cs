using Microsoft.EntityFrameworkCore;

using MyBasicWebApiDemo.Models;

namespace MyBasicWebApiDemo.Data;

public static class SeedDataExtensions
{
    public static void SeedData(this ModelBuilder builder)
    {
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        var categoryId3 = Guid.NewGuid();
        builder.Entity<Category>().HasData(
            new Category { Id = categoryId1, Name = ".NET" },
            new Category { Id = categoryId2, Name = "Cloud" },
            new Category { Id = categoryId3, Name = "DevOps" }
        );

        builder.Entity<Post>().HasData(
            // Create 30 posts
            Enumerable.Range(1, 30).Select(index => new Post
            {
                Id = Guid.NewGuid(),
                Title = $"Post {index}",
                Content = $"Post {index} content",
                CategoryId = index switch
                {
                    var n when n <= 10 => categoryId1,
                    var n when n <= 20 => categoryId2,
                    _ => categoryId3
                }
            }).ToArray()
        );


    }
}
