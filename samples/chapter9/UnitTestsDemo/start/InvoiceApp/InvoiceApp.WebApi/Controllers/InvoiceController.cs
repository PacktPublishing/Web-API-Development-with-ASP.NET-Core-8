using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Models;
using InvoiceApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(InvoiceDbContext dbContext, IEmailService emailService) : ControllerBase
{
    // Generate CRUD operations for Invoices
    // GET /api/invoice
    // GET /api/invoice/{id}
    // POST /api/invoice
    // PUT /api/invoice/{id}
    // DELETE /api/invoice/{id}
    //// PATCH /api/invoice/{id}/status

    // GET: api/Invoices
    [HttpGet]
    public async Task<ActionResult<List<Invoice>>> GetInvoicesAsync(int page = 1, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        var invoices = await dbContext.Invoices
            .Include(i => i.Contact)
            .Where(i => status == null || i.Status == status)
            .OrderByDescending(i => i.InvoiceDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return Ok(invoices);
    }

    // GET: api/Invoices/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoiceAsync(Guid id)
    {
        var invoice = await dbContext.Invoices
            .Include(i => i.Contact)
            .SingleOrDefaultAsync(i => i.Id == id);
        if (invoice == null)
        {
            return NotFound();
        }
        return Ok(invoice);
    }

    // POST: api/Invoices
    [HttpPost]
    public async Task<ActionResult<Invoice>> CreateInvoiceAsync(Invoice invoice)
    {
        invoice.Id = Guid.NewGuid();
        invoice.InvoiceNumber = GenerateInvoiceNumber();
        invoice.InvoiceDate = DateTimeOffset.UtcNow;
        invoice.DueDate = invoice.InvoiceDate.AddDays(30);
        invoice.Status = InvoiceStatus.Draft;
        invoice.InvoiceItems.ForEach(x => x.Amount = x.UnitPrice * x.Quantity);
        invoice.Amount = invoice.InvoiceItems.Sum(x => x.Amount);
        await dbContext.Invoices.AddAsync(invoice);
        await dbContext.SaveChangesAsync();
        return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
    }

    // PUT: api/Invoices/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoiceAsync(Guid id, Invoice invoice)
    {
        var existingInvoice = await dbContext.Invoices
            .SingleOrDefaultAsync(i => i.Id == id);
        if (existingInvoice == null)
        {
            return NotFound();
        }
        // Check if the invoice has correct Id
        if (!string.IsNullOrWhiteSpace(invoice.Id.ToString()) && invoice.Id != Guid.Empty && invoice.Id != id)
        {
            return BadRequest("Invoice Id cannot be changed.");
        }

        invoice.Id = id;
        dbContext.Entry(existingInvoice).CurrentValues.SetValues(invoice);
        existingInvoice.InvoiceItems.ForEach(x => x.Amount = x.UnitPrice * x.Quantity);
        existingInvoice.Amount = existingInvoice.InvoiceItems.Sum(x => x.Amount);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Invoices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoiceAsync(Guid id)
    {
        var existingInvoice = await dbContext.Invoices
            .SingleOrDefaultAsync(i => i.Id == id);
        if (existingInvoice == null)
        {
            return NotFound();
        }
        dbContext.Invoices.Remove(existingInvoice);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    // PATCH: api/Invoices/5/status
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateInvoiceStatusAsync(Guid id, InvoiceStatus status)
    {
        var existingInvoice = await dbContext.Invoices
            .SingleOrDefaultAsync(i => i.Id == id);
        if (existingInvoice == null)
        {
            return NotFound();
        }
        existingInvoice.Status = status;
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Invoices/5/send
    [HttpPost("{id}/send")]
    public async Task<IActionResult> SendInvoiceAsync(Guid id)
    {
        var existingInvoice = await dbContext.Invoices
            .Include(i => i.Contact)
            .SingleOrDefaultAsync(i => i.Id == id);
        if (existingInvoice == null)
        {
            return NotFound();
        }

        var email = emailService.GenerateInvoiceEmail(existingInvoice);
        await emailService.SendEmailAsync(email.to, email.subject, email.body);
        existingInvoice.Status = InvoiceStatus.AwaitPayment;
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    // This is just a simple way to generate a random invoice number. Please don't use this in production.
    private string GenerateInvoiceNumber()
    {
        var random = new Random();
        return $"INV-{random.Next(0, 1000000):000000}";
    }

}
