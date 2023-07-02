using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.WebApi.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceDbContext _dbContext;

    public InvoiceRepository(InvoiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Invoice?> GetInvoiceAsync(Guid id)
    {
        return await _dbContext.Invoices.Include(i => i.Contact)
            .SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesAsync(int page = 1, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        return await _dbContext.Invoices
            .Include(x => x.Contact)
            .Where(x => status == null || x.Status == status)
            .OrderByDescending(x => x.InvoiceDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByContactIdAsync(Guid contactId, int page = 1, int pageSize = 10, InvoiceStatus? status = null)
    {
        return await _dbContext.Invoices
            .Include(x => x.Contact)
            .Where(x => x.ContactId == contactId)
            .Where(x => status == null || x.Status == status)
            .OrderByDescending(x => x.InvoiceDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


    public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
        await _dbContext.Invoices.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();
        return invoice;
    }

    public async Task<Invoice?> UpdateInvoiceAsync(Invoice invoice)
    {
        var existingInvoice = await _dbContext.Invoices.FindAsync(invoice.Id);
        if (existingInvoice == null)
        {
            return null;
        }
        _dbContext.Entry(existingInvoice).CurrentValues.SetValues(invoice);
        await _dbContext.SaveChangesAsync();
        return invoice;
    }

    public Task DeleteInvoiceAsync(Guid id)
    {
        return _dbContext.Invoices.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}

