using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrimaryConstructorController(IPostService postService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await postService.GetPost(id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        await postService.CreatePost(post);
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetPosts()
    {
        var posts = await postService.GetAllPosts();
        return Ok(posts);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost(int id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        var updatedPost = await postService.UpdatePost(id, post);
        if (updatedPost == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        var post = await postService.GetPost(id);
        if (post == null)
        {
            return NotFound();
        }

        await postService.DeletePost(id);
        return NoContent();
    }
}

