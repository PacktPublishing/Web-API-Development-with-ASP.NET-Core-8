using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MyWebApiDemo.Data;
using MyWebApiDemo.Models;
using MyWebApiDemo.OpenTelemetry.Metrics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly InvoiceDbContext _dbContext;
    private readonly InvoiceMetrics _invoiceMetrics;
    private readonly Random _random = new();

    public InvoicesController(InvoiceDbContext dbContext, InvoiceMetrics invoiceMetrics)
    {
        _dbContext = dbContext;
        _invoiceMetrics = invoiceMetrics;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> Get(int page = 1, int pageSize = 10)
    {
        _invoiceMetrics.IncrementRequest();
        _invoiceMetrics.IncrementRead();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        var result = await _dbContext.Invoices.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        _invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> Get(Guid id)
    {
        _invoiceMetrics.IncrementRequest();
        _invoiceMetrics.IncrementRead();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        var result = await _dbContext.Invoices.FindAsync(id);
        if (result == null)
        {
            _invoiceMetrics.DecrementRequest();
            stopwatch.Stop();
            _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            return NotFound();
        }
        _invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> Post(Invoice invoice)
    {
        _invoiceMetrics.IncrementRequest();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        if (invoice.Id == Guid.Empty)
        {
            invoice.Id = Guid.NewGuid();
        }
        _dbContext.Invoices.Add(invoice);
        await _dbContext.SaveChangesAsync();

        // Instrumentation
        _invoiceMetrics.IncrementCreate();
        _invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return CreatedAtAction(nameof(Get), new { id = invoice.Id }, invoice);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, Invoice invoice)
    {
        _invoiceMetrics.IncrementRequest();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        if (id != invoice.Id)
        {
            _invoiceMetrics.DecrementRequest();
            stopwatch.Stop();
            _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            return BadRequest();
        }

        _dbContext.Entry(invoice).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        _invoiceMetrics.IncrementUpdate();
        _invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _invoiceMetrics.IncrementRequest();
        var stopwatch = Stopwatch.StartNew();
        await Task.Delay(_random.Next(0, 500));
        var invoice = await _dbContext.Invoices.FindAsync(id);
        if (invoice == null)
        {
            _invoiceMetrics.DecrementRequest();
            stopwatch.Stop();
            _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
            return NotFound();
        }

        _dbContext.Invoices.Remove(invoice);
        await _dbContext.SaveChangesAsync();
        _invoiceMetrics.IncrementDelete();
        _invoiceMetrics.DecrementRequest();
        stopwatch.Stop();
        _invoiceMetrics.RecordRequestDuration(stopwatch.Elapsed.TotalMilliseconds);
        return NoContent();
    }
}
