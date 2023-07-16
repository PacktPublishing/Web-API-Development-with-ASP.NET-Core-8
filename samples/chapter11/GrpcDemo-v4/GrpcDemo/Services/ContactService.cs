using Grpc.Core;

namespace GrpcDemo.Services;

public class ContactService : GrpcDemo.Contact.ContactBase
{
    private readonly ILogger<ContactService> _logger;

    public ContactService(ILogger<ContactService> logger)
    {
        _logger = logger;
    }

    public override Task<CreateContactResponse> CreateContact(CreateContactRequest request, ServerCallContext context)
    {
        // TODO: Save contact to database
        return Task.FromResult(new CreateContactResponse
        {
            ContactId = Guid.NewGuid().ToString()
        });
    }
}

