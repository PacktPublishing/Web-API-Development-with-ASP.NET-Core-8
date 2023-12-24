using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MyBasicWebApiDemo.Data;
using MyBasicWebApiDemo.Models;

namespace MyBasicWebApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(SampleDbContext context) : ControllerBase
{
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    //{
    //    return await _context.Posts.ToListAsync();
    //}

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<Post>>> GetPosts(int pageIndex = 1, int pageSize = 10)
    {
        var posts = context.Posts.AsQueryable().AsNoTracking();
        var count = await posts.CountAsync();
        var items = await posts.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var result = new PaginatedList<Post>(items, count, pageIndex, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Get a post by id
    /// </summary>
    /// <param name="id">The id of the post</param>
    /// <returns>The post</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Post>> GetPost(Guid id)
    {
        var post = await context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutPost(Guid id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        context.Entry(post).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Post>> PostPost(Post post)
    {
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetPost", new { id = post.Id }, post);
    }

    private bool PostExists(Guid id)
    {
        return context.Posts.Any(e => e.Id == id);
    }
}
