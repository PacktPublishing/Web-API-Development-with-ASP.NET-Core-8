namespace MinimalApiDemo.Services;

public class PostService : IPostService
{
    // Use a static list for demo purposes
    private static readonly List<Post> AllPosts = new()
    {
        new() { Id = 1, Title = "First Post", Content = "Hello World" },
        new() { Id = 2, Title = "Second Post", Content = "Hello Again" },
        new() { Id = 3, Title = "Third Post", Content = "Goodbye World" },
    };


    public Task<Post?> GetPostAsync(int id)
    {
        var post = AllPosts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(post);
    }

    public Task<List<Post>> GetPostsAsync()
    {
        return Task.FromResult(AllPosts);
    }

    public Task<Post> CreatePostAsync(Post post)
    {
        post.Id = AllPosts.Max(p => p.Id) + 1;
        AllPosts.Add(post);
        return Task.FromResult(post);
    }

    public Task<Post> UpdatePostAsync(int id, Post post)
    {
        var index = AllPosts.FindIndex(p => p.Id == id);
        if (index == -1)
        {
            throw new KeyNotFoundException();
        }
        AllPosts[index] = post;
        return Task.FromResult(post);
    }

    public Task DeletePostAsync(int id)
    {
        var post = AllPosts.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new KeyNotFoundException();
        }
        AllPosts.Remove(post);
        return Task.CompletedTask;
    }
}
