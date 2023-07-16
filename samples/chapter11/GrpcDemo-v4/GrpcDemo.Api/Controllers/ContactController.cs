using Microsoft.AspNetCore.Mvc;

namespace GrpcDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly Contact.ContactClient _client;
    private readonly ILogger<ContactController> _logger;

    public ContactController(Contact.ContactClient client, ILogger<ContactController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactRequest request)
    {
        var reply = await _client.CreateContactAsync(request);
        return Ok(reply);
    }
}
