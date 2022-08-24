using MyFirstApi.Models;

namespace MyFirstApi.Services;

public class PostsService
{
    private static readonly List<Post> AllPosts = new();

    public Task CreatePost(Post item)
    {
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
}

