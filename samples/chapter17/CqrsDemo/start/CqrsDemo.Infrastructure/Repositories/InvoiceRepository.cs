using CqrsDemo.Core.Models;
using CqrsDemo.Core.Repositories;
using CqrsDemo.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Infrastructure.Repositories;
public class InvoiceRepository(AppDbContext dbContext) : IInvoiceRepository
{
    public Task<Invoice?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Invoices
            .Include(x => x.InvoiceItems)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<List<Invoice>> GetAllListAsync(CancellationToken cancellationToken)
    {
        return dbContext.Invoices.AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task<List<Invoice>> GetPagedListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return dbContext.Invoices.AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Invoice?> AddAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        await dbContext.Invoices.AddAsync(invoice, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        var result = await dbContext.Invoices.AsNoTracking()
            .Include(x => x.InvoiceItems)
            .SingleOrDefaultAsync(x => x.Id == invoice.Id, cancellationToken);
        return result;
    }

    public async Task<Invoice?> UpdateAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        var existingInvoice = await dbContext.Invoices
            .Include(x => x.InvoiceItems)
            .SingleOrDefaultAsync(x => x.Id == invoice.Id, cancellationToken);
        if (existingInvoice == null)
        {
            throw new InvalidOperationException($"Invoice with id {invoice.Id} not found.");
        }
        dbContext.Entry(existingInvoice).CurrentValues.SetValues(invoice);
        existingInvoice.InvoiceItems = invoice.InvoiceItems;
        await dbContext.SaveChangesAsync(cancellationToken);
        return invoice;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var invoice = await dbContext.Invoices.FindAsync(keyValues: new object?[] { id }, cancellationToken: cancellationToken);
        if (invoice == null)
        {
            throw new InvalidOperationException($"Invoice with id {id} not found.");
        }
        dbContext.Invoices.Remove(invoice);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
