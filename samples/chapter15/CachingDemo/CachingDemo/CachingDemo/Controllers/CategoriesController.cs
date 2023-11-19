using CachingDemo.Models;
using CachingDemo.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CachingDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// This method returns all categories.
    /// The response of this endpoint is cached for 60 seconds on client-side.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var result = await _categoryService.GetCategoriesAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("favorites/{userId}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetFavoritesCategories(int userId)
    {
        try
        {
            var result = await _categoryService.GetFavoritesCategoriesAsync(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    [OutputCache(PolicyName = "Expire600")]
    public async Task<ActionResult<Category?>> Get(int id)
    {
        var result = await _categoryService.GetCategoryAsync(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public Task<Category> Post(Category category)
    {
        return _categoryService.AddCategoryAsync(category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category?>> Put(int id, Category category)
    {
        var existingCategory = await _categoryService.GetCategoryAsync(id);
        if (existingCategory is null)
        {
            return NotFound();
        }
        return await _categoryService.UpdateCategoryAsync(category);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var existingCategory = await _categoryService.GetCategoryAsync(id);
        if (existingCategory is null)
        {
            return NotFound();
        }
        return await _categoryService.DeleteCategoryAsync(id);
    }

}
