using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly InvoiceDbContext _dbContext;
    public ContactController(InvoiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Contacts
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetContactsAsync(int page = 1, int pageSize = 10)
    {
        var contacts = await _dbContext.Contacts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return Ok(contacts);
    }

    // GET: api/Contacts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContactAsync(Guid id)
    {
        var contact = await _dbContext.Contacts
            .SingleOrDefaultAsync(c => c.Id == id);
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
    }

    // POST: api/Contacts
    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContactAsync(Contact contact)
    {
        contact.Id = Guid.NewGuid();
        await _dbContext.Contacts.AddAsync(contact);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
    }

    // PUT: api/Contacts/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContactAsync(Guid id, Contact contact)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        var existingContact = await _dbContext.Contacts
            .SingleOrDefaultAsync(c => c.Id == id);
        if (existingContact == null)
        {
            return NotFound();
        }
        _dbContext.Entry(existingContact).CurrentValues.SetValues(contact);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return NoContent();
    }

    // DELETE: api/Contacts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactAsync(Guid id)
    {
        var existingContact = await _dbContext.Contacts
            .SingleOrDefaultAsync(c => c.Id == id);
        if (existingContact == null)
        {
            return NotFound();
        }
        _dbContext.Contacts.Remove(existingContact);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    // Get invoices for a contact
    // GET: api/Contacts/5/Invoices
    [HttpGet("{id}/invoices")]
    public async Task<ActionResult<List<Invoice>>> GetInvoicesAsync(Guid id, int page = 0, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        var invoices = await _dbContext.Invoices
            .Where(i => i.ContactId == id)
            .Where(i => status == null || i.Status == status)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return Ok(invoices);
    }
}
