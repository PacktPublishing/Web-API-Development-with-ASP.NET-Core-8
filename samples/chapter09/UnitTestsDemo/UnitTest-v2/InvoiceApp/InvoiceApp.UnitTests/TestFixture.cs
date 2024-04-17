using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Models;

namespace InvoiceApp.UnitTests;
public class TestFixture
{
    public List<Invoice> Invoices { get; set; } = new();
    public List<Contact> Contacts { get; set; } = new();

    public TestFixture()
    {
        InitializeDatabase();
    }

    public void InitializeDatabase()
    {
        // Create a few Contacts
        Contacts = new List<Contact>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com"
            }
        };
        // Create a few Invoices
        Invoices = new List<Invoice>
        {
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-001",
                Amount = 500,
                DueDate = DateTimeOffset.Now.AddDays(30),
                Contact = Contacts[0],
                Status = InvoiceStatus.AwaitPayment,
                InvoiceDate = DateTimeOffset.Now,
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Item 1",
                        Quantity = 1,
                        UnitPrice = 100,
                        Amount = 100
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Item 2",
                        Quantity = 2,
                        UnitPrice = 200,
                        Amount = 400
                    }
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceNumber = "INV-002",
                Amount = 1000,
                DueDate = DateTimeOffset.Now.AddDays(30),
                Contact = Contacts[1],
                Status = InvoiceStatus.Draft,
                InvoiceDate = DateTimeOffset.Now,
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Item 1",
                        Quantity = 2,
                        UnitPrice = 100,
                        Amount = 200
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Description = "Item 2",
                        Quantity = 4,
                        UnitPrice = 200,
                        Amount = 800
                    }
                }
            }
        };
    }
}
