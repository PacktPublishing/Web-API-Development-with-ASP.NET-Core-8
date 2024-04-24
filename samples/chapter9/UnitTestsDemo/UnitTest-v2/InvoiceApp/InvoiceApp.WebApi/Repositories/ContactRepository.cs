using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.WebApi.Repositories;

public class ContactRepository(InvoiceDbContext dbContext) : IContactRepository
{
    public async Task<Contact?> GetContactAsync(Guid id)
    {
        return await dbContext.Contacts.FindAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetContactsAsync(int page = 1, int pageSize = 10)
    {
        return await dbContext.Contacts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        await dbContext.Contacts.AddAsync(contact);
        await dbContext.SaveChangesAsync();
        return contact;
    }

    public async Task<Contact?> UpdateContactAsync(Contact contact)
    {
        var existingContact = await dbContext.Contacts
            .SingleOrDefaultAsync(c => c.Id == contact.Id);
        if (existingContact == null)
        {
            return null;
        }
        dbContext.Entry(existingContact).CurrentValues.SetValues(contact);
        await dbContext.SaveChangesAsync();
        return existingContact;
    }

    public Task DeleteContactAsync(Guid id)
    {
        return dbContext.Contacts.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
