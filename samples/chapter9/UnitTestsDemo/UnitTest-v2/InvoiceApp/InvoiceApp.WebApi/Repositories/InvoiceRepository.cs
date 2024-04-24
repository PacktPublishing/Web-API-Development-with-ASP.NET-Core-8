using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.WebApi.Repositories;

public class InvoiceRepository(InvoiceDbContext dbContext) : IInvoiceRepository
{
    public Task<Invoice?> GetInvoiceAsync(Guid id)
    {
        return dbContext.Invoices.Include(i => i.Contact)
            .SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesAsync(int page = 1, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        return await dbContext.Invoices
            .Include(x => x.Contact)
            .Where(x => status == null || x.Status == status)
            .OrderByDescending(x => x.InvoiceDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetInvoicesByContactIdAsync(Guid contactId, int page = 1, int pageSize = 10, InvoiceStatus? status = null)
    {
        return await dbContext.Invoices
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
        var contact = await dbContext.Contacts.FindAsync(invoice.ContactId);
        if (contact == null)
        {
            throw new ArgumentException("Contact not found", nameof(invoice.ContactId));
        }
        invoice.Contact = contact;
        await dbContext.Invoices.AddAsync(invoice);
        await dbContext.SaveChangesAsync();
        return invoice;
    }

    public async Task<Invoice?> UpdateInvoiceAsync(Invoice invoice)
    {
        var existingInvoice = await dbContext.Invoices.FindAsync(invoice.Id);
        if (existingInvoice == null)
        {
            return null;
        }
        var contact = await dbContext.Contacts.FindAsync(invoice.ContactId);
        if (contact == null)
        {
            throw new ArgumentException("Contact not found", nameof(invoice.ContactId));
        }
        invoice.Contact = contact;
        dbContext.Entry(existingInvoice).CurrentValues.SetValues(invoice);
        await dbContext.SaveChangesAsync();
        return invoice;
    }

    public Task DeleteInvoiceAsync(Guid id)
    {
        return dbContext.Invoices.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}

