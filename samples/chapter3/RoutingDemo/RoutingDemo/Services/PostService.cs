using RoutingDemo.Models;

namespace RoutingDemo.Services;

public class PostsService : IPostService
{
    private static readonly List<Post> AllPosts = new()
    {
        new Post { Id = 1, UserId = 1, Title = "First Post", Body = "This is the first post" },
        new Post { Id = 2, UserId = 1, Title = "Second Post", Body = "This is the second post" },
        new Post { Id = 3, UserId = 2, Title = "Third Post", Body = "This is the third post" },
    };

    public Task CreatePost(Post item)
    {
        item.Id = AllPosts.Max(x => x.Id) + 1;
        item.IsPublished = true;
        AllPosts.Add(item);
        return Task.CompletedTask;
    }

    public Task<Post?> UpdatePost(int id, Post item)
    {
        var post = AllPosts.FirstOrDefault(x => x.Id == id);
        if (post != null)
        {
            post.Title = item.Title;
            post.Body = item.Body;
            post.UserId = item.UserId;
        }
        return Task.FromResult(post);
    }

    public Task<Post?> GetPost(int id)
    {
        return Task.FromResult(AllPosts.FirstOrDefault(x => x.Id == id));
    }

    public Task<List<Post>> GetAllPosts()
    {
        return Task.FromResult(AllPosts);
    }

    public Task DeletePost(int id)
    {
        var post = AllPosts.FirstOrDefault(x => x.Id == id);
        if (post != null)
        {
            AllPosts.Remove(post);
        }

        return Task.CompletedTask;
    }

    public Task<Post?> PublishPost(int id)
    {
        var post = AllPosts.FirstOrDefault(x => x.Id == id);
        if (post != null)
        {
            post.IsPublished = true;
        }

        return Task.FromResult(post);
    }

    public Task<Post?> UnpublishPost(int id)
    {
        var post = AllPosts.FirstOrDefault(x => x.Id == id);
        if (post != null)
        {
            post.IsPublished = false;
        }

        return Task.FromResult(post);
    }

    public Task<List<Post>> GetPostsByUserId(int userId)
    {
        return Task.FromResult(AllPosts.Where(x => x.UserId == userId).ToList());
    }

    public Task<List<Post>> GetPosts(int pageIndex, int pageSize)
    {
        return Task.FromResult(AllPosts.Skip(pageIndex * pageSize).Take(pageSize).ToList());
    }

    public Task<List<Post>> SearchPosts(string keyword)
    {
        return Task.FromResult(AllPosts.Where(x => x.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList());
    }
}

