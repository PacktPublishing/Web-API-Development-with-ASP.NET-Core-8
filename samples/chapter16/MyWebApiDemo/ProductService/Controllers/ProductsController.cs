using Microsoft.AspNetCore.Mvc;

using MyWebApiDemo.Core.Models;
using MyWebApiDemo.Core.Services;

namespace ProductService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await productService.GetProductAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Post(Product product)
    {
        try
        {
            var newProduct = await productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest("Product id mismatch.");
        }

        if (await productService.GetProductAsync(id) == null)
        {
            return NotFound();
        }

        try
        {
            var updatedProduct = await productService.UpdateProductAsync(product);
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
