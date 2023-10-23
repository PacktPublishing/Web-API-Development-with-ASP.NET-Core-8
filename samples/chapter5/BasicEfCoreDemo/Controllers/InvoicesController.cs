using BasicEfCoreDemo.Data;
using BasicEfCoreDemo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BasicEfCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceDbContext _context;

        public InvoicesController(InvoiceDbContext context)
        {
            _context = context;
        }

        // The following code snippet shows how to retrieve all invoices from the database.
        // GET: api/Invoices
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        // {
        //     if (_context.Invoices == null)
        //     {
        //         return NotFound();
        //     }
        //     return await _context.Invoices.ToListAsync();
        // }

        // The following code snippet shows how to retrieve all invoices from the database with a specific status.
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices(InvoiceStatus status)
        // {
        //     // Omitted for brevity 
        //     return await _context.Invoices.Where(x => x.Status == status).ToListAsync();
        // }

        // The following code snippet shows how to retrieve pages of invoices from the database.s
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices(int page = 1, int pageSize = 10, InvoiceStatus? status = null)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            // The AsQueryable() method is not required, as the DbSet<TEntity> class implements the IQueryable<TEntity> interface.
            return await _context.Invoices.AsQueryable().Where(x => status == null || x.Status == status)
                        .OrderByDescending(x => x.InvoiceDate)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            //_context.Entry(invoice).State = EntityState.Modified;

            try
            {
                var invoiceToUpdate = await _context.Invoices.FindAsync(id);
                if (invoiceToUpdate == null)
                {
                    return NotFound();
                }
                // invoiceToUpdate.InvoiceNumber = invoice.InvoiceNumber;
                // invoiceToUpdate.ContactName = invoice.ContactName;
                // invoiceToUpdate.Description = invoice.Description;
                // invoiceToUpdate.Amount = invoice.Amount;
                // invoiceToUpdate.InvoiceDate = invoice.InvoiceDate;
                // invoiceToUpdate.DueDate = invoice.DueDate;
                // invoiceToUpdate.Status = invoice.Status;

                // Update only the properties that have changed
                _context.Entry(invoiceToUpdate).CurrentValues.SetValues(invoice);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'InvoiceDbContext.Invoices'  is null.");
            }
            _context.Invoices.Add(invoice);
            // The preceding code is equivalent to the following code:
            //_context.Entry(invoice).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(Guid id)
        {
            return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
