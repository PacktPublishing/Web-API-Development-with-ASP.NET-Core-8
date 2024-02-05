using Microsoft.AspNetCore.Mvc;

namespace GrpcDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController(Contact.ContactClient client, ILogger<ContactController> logger)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactRequest request)
    {
        var reply = await client.CreateContactAsync(request);
        return Ok(reply);
    }
}
