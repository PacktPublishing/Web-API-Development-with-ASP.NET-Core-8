using Grpc.Net.Client;

namespace GrpcDemo.Client;
internal class InvoiceClient
{
    public async Task CreateContactAsync()
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7179");
        var client = new Contact.ContactClient(channel);
        var reply = await client.CreateContactAsync(new CreateContactRequest()
        {
            Email = "abc@abc.com",
            FirstName = "John",
            LastName = "Doe",
            IsActive = true,
            Phone = "1234567890",
            YearOfBirth = 1980
        });
        Console.WriteLine("Created Contact: " + reply.ContactId);
        Console.ReadKey();
    }
}
