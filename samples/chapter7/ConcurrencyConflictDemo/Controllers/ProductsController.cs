using ConcurrencyConflictDemo.Data;
using ConcurrencyConflictDemo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyConflictDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(SampleDbContext context) : ControllerBase
    {
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (context.Products == null)
            {
                return NotFound();
            }
            return await context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (context.Products == null)
            {
                return NotFound();
            }
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (context.Products == null)
            {
                return Problem("Entity set 'SampleDbContext.Products' is null.");
            }
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // Sell product by id and quantity
        [HttpPost("{id}/sell/{quantity}")]
        public async Task<ActionResult<Product>> SellProduct(int id, int quantity, int delay = 0)
        {
            if (context.Products == null)
            {
                return Problem("Entity set 'SampleDbContext.Products'  is null.");
            }
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (product.Inventory < quantity)
            {
                return Problem("Not enough inventory.");
            }
            await Task.Delay(TimeSpan.FromSeconds(delay));
            product.Inventory -= quantity;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Do not forget to log the error
                return Conflict($"Concurrency conflict for Product {product.Id}.");
            }

            return product;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (context.Products == null)
            {
                return NotFound();
            }
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
