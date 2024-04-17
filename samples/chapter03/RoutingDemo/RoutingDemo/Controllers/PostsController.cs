using Microsoft.AspNetCore.Mvc;

using RoutingDemo.Models;
using RoutingDemo.Services;

namespace RoutingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Route("api/posts")]
//[Route("api/some-posts-whatever")]
public class PostsController : ControllerBase
{
    private readonly PostsService _postsService;

    public PostsController()
    {
        _postsService = new PostsService();
    }

    [HttpGet("{id:int}")] // api/posts/1
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postsService.GetPost(id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]  // api/posts
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        await _postsService.CreatePost(post);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpGet] // api/posts
    public async Task<ActionResult<List<Post>>> GetPosts()
    {
        var posts = await _postsService.GetAllPosts();
        return Ok(posts);
    }

    [HttpPut("{id}")] // api/posts/1
    public async Task<ActionResult> UpdatePost(int id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        var updatedPost = await _postsService.UpdatePost(id, post);
        if (updatedPost == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpDelete("{id}")] // api/posts/1
    public async Task<ActionResult> DeletePost(int id)
    {
        var post = await _postsService.GetPost(id);
        if (post == null)
        {
            return NotFound();
        }

        await _postsService.DeletePost(id);
        return NoContent();
    }

    [HttpPut("{id}/publish")] // api/posts/1/publish
    public async Task<ActionResult> PublishPost(int id)
    {
        var post = await _postsService.PublishPost(id);
        if (post == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}/unpublish")] // api/posts/1/unpublish
    public async Task<ActionResult> UnpublishPost(int id)
    {
        var post = await _postsService.UnpublishPost(id);
        if (post == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("user/{userId}")] // api/posts/user/1
    public async Task<ActionResult<List<Post>>> GetPostsByUserId(int userId)
    {
        var posts = await _postsService.GetPostsByUserId(userId);
        return Ok(posts);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<List<Post>>> GetPosts([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        var posts = await _postsService.GetPosts(pageIndex, pageSize);
        return Ok(posts);
    }

    [HttpPost("search")]
    public async Task<ActionResult<Post>> SearchPosts([FromBody] string keyword)
    {
        var posts = await _postsService.SearchPosts(keyword);
        return Ok(posts);
    }
}


