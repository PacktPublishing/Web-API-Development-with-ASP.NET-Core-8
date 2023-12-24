using CachingDemo.Models;
using CachingDemo.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CachingDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    /// <summary>
    /// This method returns all categories.
    /// The response of this endpoint is cached for 60 seconds on client-side.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var result = await categoryService.GetCategoriesAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("favorites/{userId}")]
    public async Task<ActionResult<IEnumerable<Category>>> GetFavoritesCategories(int userId)
    {
        try
        {
            var result = await categoryService.GetFavoritesCategoriesAsync(userId);
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
        var result = await categoryService.GetCategoryAsync(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public Task<Category> Post(Category category)
    {
        return categoryService.AddCategoryAsync(category);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category?>> Put(int id, Category category)
    {
        var existingCategory = await categoryService.GetCategoryAsync(id);
        if (existingCategory is null)
        {
            return NotFound();
        }
        return await categoryService.UpdateCategoryAsync(category);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var existingCategory = await categoryService.GetCategoryAsync(id);
        if (existingCategory is null)
        {
            return NotFound();
        }
        return await categoryService.DeleteCategoryAsync(id);
    }

}
