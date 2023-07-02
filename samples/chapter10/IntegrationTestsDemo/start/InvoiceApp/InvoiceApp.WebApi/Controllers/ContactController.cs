using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    public ContactController(IContactRepository contactRepository, IInvoiceRepository invoiceRepository)
    {
        _contactRepository = contactRepository;
        _invoiceRepository = invoiceRepository;
    }

    // GET: api/Contacts
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetContactsAsync(int page = 1, int pageSize = 10)
    {
        var contacts = await _contactRepository.GetContactsAsync(page, pageSize);
        return Ok(contacts);
    }

    // GET: api/Contacts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContactAsync(Guid id)
    {
        var contact = await _contactRepository.GetContactAsync(id);
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
        await _contactRepository.CreateContactAsync(contact);
        return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
    }

    // PUT: api/Contacts/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContactAsync(Guid id, Contact contact)
    {
        contact.Id = id;
        var updateContact = await _contactRepository.UpdateContactAsync(contact);
        if (updateContact == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE: api/Contacts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactAsync(Guid id)
    {
        await _contactRepository.DeleteContactAsync(id);
        return NoContent();
    }

    // Get invoices for a contact
    // GET: api/Contacts/5/Invoices
    [HttpGet("{id}/invoices")]
    public async Task<ActionResult<List<Invoice>>> GetInvoicesAsync(Guid id, int page = 0, int pageSize = 10,
        InvoiceStatus? status = null)
    {
        var invoices = await _invoiceRepository.GetInvoicesByContactIdAsync(id, page, pageSize, status);
        return Ok(invoices);
    }
}
