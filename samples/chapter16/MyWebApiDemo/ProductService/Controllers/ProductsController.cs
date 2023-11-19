using Microsoft.AspNetCore.Mvc;

using MyWebApiDemo.Core.Models;
using MyWebApiDemo.Core.Services;

namespace ProductService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) =>
        _productService = productService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _productService.GetProductAsync(id);
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
            var newProduct = await _productService.CreateProductAsync(product);
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

        if (await _productService.GetProductAsync(id) == null)
        {
            return NotFound();
        }

        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(product);
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
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
