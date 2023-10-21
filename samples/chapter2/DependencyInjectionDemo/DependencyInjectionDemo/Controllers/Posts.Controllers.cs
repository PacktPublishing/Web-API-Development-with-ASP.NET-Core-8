using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;

using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postsService;

    public PostsController(IPostService postService)
    {
        _postsService = postService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postsService.GetPost(id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        await _postsService.CreatePost(post);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts()
    {
        var posts = await _postsService.GetAllPosts();
        return Ok(posts);
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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
}
