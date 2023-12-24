using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApiDemo.Data;
using MyWebApiDemo.Models;
using MyWebApiDemo.OpenTelemetry.Metrics;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController(InvoiceDbContext dbContext, InvoiceMetrics invoiceMetrics) : ControllerBase
{
    private readonly Random _random = new();

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> Get(int page = 1, int pageSize = 10)
    {
        invoiceMetrics.IncrementRequest();
        invoiceMetrics.IncrementRead();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        var result = await dbContext.Invoices.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> Get(Guid id)
    {
        invoiceMetrics.IncrementRequest();
        invoiceMetrics.IncrementRead();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        var result = await dbContext.Invoices.FindAsync(id);
        if (result == null)
        {
            invoiceMetrics.DecrementRequest();
            stopwatch.Stop();
            invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            return NotFound();
        }
        invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> Post(Invoice invoice)
    {
        invoiceMetrics.IncrementRequest();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        if (invoice.Id == Guid.Empty)
        {
            invoice.Id = Guid.NewGuid();
        }
        dbContext.Invoices.Add(invoice);
        await dbContext.SaveChangesAsync();

        // Instrumentation
        invoiceMetrics.IncrementCreate();
        invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return CreatedAtAction(nameof(Get), new { id = invoice.Id }, invoice);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, Invoice invoice)
    {
        invoiceMetrics.IncrementRequest();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        if (id != invoice.Id)
        {
            invoiceMetrics.DecrementRequest();
            stopwatch.Stop();
            invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            return BadRequest();
        }

        dbContext.Entry(invoice).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        invoiceMetrics.IncrementUpdate();
        invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        invoiceMetrics.IncrementRequest();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        var invoice = await dbContext.Invoices.FindAsync(id);
        if (invoice == null)
        {
            invoiceMetrics.DecrementRequest();
            stopwatch.Stop();
            invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            return NotFound();
        }

        dbContext.Invoices.Remove(invoice);
        await dbContext.SaveChangesAsync();
        invoiceMetrics.IncrementDelete();
        invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return NoContent();
    }
}
