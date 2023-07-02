using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    // Generate CRUD operations for Invoices
    // GET /api/invoice
    // GET /api/invoice/{id}
    // POST /api/invoice
    // PUT /api/invoice/{id}
    // DELETE /api/invoice/{id}
    // PATCH /api/invoice/{id}/status

    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IEmailService _emailService;

    public InvoiceController(IInvoiceRepository invoiceRepository, IEmailService emailService)
    {
        _invoiceRepository = invoiceRepository;
        _emailService = emailService;
    }

    // GET: api/Invoices
    [HttpGet]
    public async Task<ActionResult<List<Invoice>>> GetInvoicesAsync(int page = 1, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        var invoices = await _invoiceRepository.GetInvoicesAsync(page, pageSize, status);
        return Ok(invoices);
    }

    // GET: api/Invoices/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoiceAsync(Guid id)
    {
        var invoice = await _invoiceRepository.GetInvoiceAsync(id);
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
        try
        {
            await _invoiceRepository.CreateInvoiceAsync(invoice);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
    }

    // PUT: api/Invoices/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoiceAsync(Guid id, Invoice invoice)
    {
        var existingInvoice = await _invoiceRepository.GetInvoiceAsync(id);
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
        await _invoiceRepository.UpdateInvoiceAsync(invoice);
        return NoContent();
    }

    // DELETE: api/Invoices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoiceAsync(Guid id)
    {
        await _invoiceRepository.DeleteInvoiceAsync(id);
        return NoContent();
    }

    // PATCH: api/Invoices/5/status
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateInvoiceStatusAsync(Guid id, InvoiceStatus status)
    {
        var invoice = await _invoiceRepository.GetInvoiceAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        invoice.Status = status;
        await _invoiceRepository.UpdateInvoiceAsync(invoice);
        return NoContent();
    }

    // POST: api/Invoices/5/send
    [HttpPost("{id}/send")]
    public async Task<IActionResult> SendInvoiceAsync(Guid id)
    {
        var existingInvoice = await _invoiceRepository.GetInvoiceAsync(id);
        if (existingInvoice == null)
        {
            return NotFound();
        }

        var (to, subject, body) = _emailService.GenerateInvoiceEmail(existingInvoice);
        try
        {
            await _emailService.SendEmailAsync(to, subject, body);
            existingInvoice.Status = InvoiceStatus.AwaitPayment;
        }
        catch
        {
            return BadRequest("Failed to send email.");
        }
        await _invoiceRepository.UpdateInvoiceAsync(existingInvoice);
        return NoContent();
    }

    // This is just a simple way to generate a random invoice number. Please don't use this in production.
    private string GenerateInvoiceNumber()
    {
        var random = new Random();
        return $"INV-{random.Next(0, 1000000):000000}";
    }
}
