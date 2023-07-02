using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.WebApi.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly InvoiceDbContext _dbContext;

    public ContactRepository(InvoiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Contact?> GetContactAsync(Guid id)
    {
        return await _dbContext.Contacts.FindAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync(int page = 1, int pageSize = 10)
    {
        return await _dbContext.Contacts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync();
        return contact;
    }

    public async Task<Contact?> UpdateContactAsync(Contact contact)
    {
        var existingContact = await _dbContext.Contacts
            .SingleOrDefaultAsync(c => c.Id == contact.Id);
        if (existingContact == null)
        {
            return null;
        }
        _dbContext.Entry(existingContact).CurrentValues.SetValues(contact);
        await _dbContext.SaveChangesAsync();
        return existingContact;
    }

    public Task DeleteContactAsync(Guid id)
    {
        return _dbContext.Contacts.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
