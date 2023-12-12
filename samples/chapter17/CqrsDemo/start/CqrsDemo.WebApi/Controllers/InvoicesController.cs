using CqrsDemo.Core.Models.Dto;
using CqrsDemo.Core.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CqrsDemo.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceWithoutItemsDto>>> GetInvoices()
    {
        var invoices = await invoiceService.GetAllListAsync();
        return Ok(invoices);
    }

    [HttpGet]
    [Route("paged")]
    public async Task<ActionResult<IEnumerable<InvoiceWithoutItemsDto>>> GetInvoices(int page, int pageSize)
    {
        var invoices = await invoiceService.GetPagedListAsync(page, pageSize);
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoice(Guid id)
    {
        var invoice = await invoiceService.GetAsync(id);
        return invoice == null ? NotFound() : Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> CreateInvoice(CreateOrUpdateInvoiceDto invoiceDto)
    {
        var invoice = await invoiceService.AddAsync(invoiceDto);
        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvoice(Guid id, CreateOrUpdateInvoiceDto invoiceDto)
    {
        try
        {
            await invoiceService.UpdateAsync(id, invoiceDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(Guid id)
    {
        try
        {
            await invoiceService.DeleteAsync(id);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        return NoContent();
    }
}
