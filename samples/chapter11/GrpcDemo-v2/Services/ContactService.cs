using Grpc.Core;

namespace GrpcDemo.Services;

public class ContactService(ILogger<ContactService> logger) : GrpcDemo.Contact.ContactBase
{
    private readonly ILogger<ContactService> _logger = logger;

    public override Task<CreateContactResponse> CreateContact(CreateContactRequest request, ServerCallContext context)
    {
        // TODO: Save contact to database
        return Task.FromResult(new CreateContactResponse
        {
            ContactId = Guid.NewGuid().ToString()
        });
    }
}

