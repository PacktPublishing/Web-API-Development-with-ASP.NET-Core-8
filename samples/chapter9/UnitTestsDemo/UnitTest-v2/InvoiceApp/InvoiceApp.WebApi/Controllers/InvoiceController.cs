using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(IInvoiceRepository invoiceRepository, IEmailService emailService)
    : ControllerBase
{
    // Generate CRUD operations for Invoices
    // GET /api/invoice
    // GET /api/invoice/{id}
    // POST /api/invoice
    // PUT /api/invoice/{id}
    // DELETE /api/invoice/{id}
    // PATCH /api/invoice/{id}/status

    // GET: api/Invoices
    [HttpGet]
    public async Task<ActionResult<List<Invoice>>> GetInvoicesAsync(int page = 1, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        var invoices = await invoiceRepository.GetInvoicesAsync(page, pageSize, status);
        return Ok(invoices);
    }

    // GET: api/Invoices/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoiceAsync(Guid id)
    {
        var invoice = await invoiceRepository.GetInvoiceAsync(id);
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
        await invoiceRepository.CreateInvoiceAsync(invoice);
        return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
    }

    // PUT: api/Invoices/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoiceAsync(Guid id, Invoice invoice)
    {
        var existingInvoice = await invoiceRepository.GetInvoiceAsync(id);
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
        invoice.InvoiceItems.ForEach(x => x.Amount = x.UnitPrice * x.Quantity);
        invoice.Amount = existingInvoice.InvoiceItems.Sum(x => x.Amount);
        await invoiceRepository.UpdateInvoiceAsync(invoice);
        return NoContent();
    }

    // DELETE: api/Invoices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoiceAsync(Guid id)
    {
        await invoiceRepository.DeleteInvoiceAsync(id);
        return NoContent();
    }

    // PATCH: api/Invoices/5/status
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateInvoiceStatusAsync(Guid id, InvoiceStatus status)
    {
        var invoice = await invoiceRepository.GetInvoiceAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        invoice.Status = status;
        await invoiceRepository.UpdateInvoiceAsync(invoice);
        return NoContent();
    }

    // POST: api/Invoices/5/send
    [HttpPost("{id}/send")]
    public async Task<IActionResult> SendInvoiceAsync(Guid id)
    {
        var existingInvoice = await invoiceRepository.GetInvoiceAsync(id);
        if (existingInvoice == null)
        {
            return NotFound();
        }

        var (to, subject, body) = emailService.GenerateInvoiceEmail(existingInvoice);
        try
        {
            await emailService.SendEmailAsync(to, subject, body);
            existingInvoice.Status = InvoiceStatus.AwaitPayment;
        }
        catch
        {
            return BadRequest("Failed to send email.");
        }
        await invoiceRepository.UpdateInvoiceAsync(existingInvoice);
        return NoContent();
    }

    // This is just a simple way to generate a random invoice number. Please don't use this in production.
    private string GenerateInvoiceNumber()
    {
        var random = new Random();
        return $"INV-{random.Next(0, 1000000):000000}";
    }
}
