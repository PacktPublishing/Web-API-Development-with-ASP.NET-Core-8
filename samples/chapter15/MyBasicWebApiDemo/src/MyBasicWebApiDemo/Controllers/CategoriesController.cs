using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MyBasicWebApiDemo.Data;
using MyBasicWebApiDemo.Models;

namespace MyBasicWebApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly SampleDbContext _context;

    public CategoriesController(SampleDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutCategory(Guid id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCategory", new { id = category.Id }, category);
    }

    private bool CategoryExists(Guid id)
    {
        return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
