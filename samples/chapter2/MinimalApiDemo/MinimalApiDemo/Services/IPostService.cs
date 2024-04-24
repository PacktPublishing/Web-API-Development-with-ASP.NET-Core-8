namespace MinimalApiDemo.Services;

public interface IPostService
{
    Task<Post?> GetPostAsync(int id);
    Task<List<Post>> GetPostsAsync();
    Task<Post> CreatePostAsync(Post post);
    Task<Post> UpdatePostAsync(int id, Post post);
    Task DeletePostAsync(int id);
}
