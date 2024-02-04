using EfCoreRelationshipsDemo.Data;
using EfCoreRelationshipsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(SampleDbContext context) : ControllerBase
    {
        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            if (context.Contacts == null)
            {
                return NotFound();
            }

            return await context.Contacts.Include(x => x.Address).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(Guid id)
        {
            if (context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(Guid id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            context.Entry(contact).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            if (context.Contacts == null)
            {
                return Problem("Entity set 'SampleDbContext.Contacts'  is null.");
            }

            context.Contacts.Add(contact);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            if (context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(Guid id)
        {
            return (context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
