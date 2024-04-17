using RoutingDemo.Models;

namespace RoutingDemo.Services;

public interface IPostService
{
    Task CreatePost(Post item);
    Task<Post?> UpdatePost(int id, Post item);
    Task<Post?> GetPost(int id);
    Task<List<Post>> GetAllPosts();
    Task DeletePost(int id);
    Task<Post?> PublishPost(int id);
    Task<Post?> UnpublishPost(int id);
    Task<List<Post>> GetPostsByUserId(int userId);
    Task<List<Post>> GetPosts(int pageIndex, int pageSize);
    Task<List<Post>> SearchPosts(string keyword);
}
