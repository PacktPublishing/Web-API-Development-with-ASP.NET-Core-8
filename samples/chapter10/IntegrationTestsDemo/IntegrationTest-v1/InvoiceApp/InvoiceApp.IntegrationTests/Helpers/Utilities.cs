using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.IntegrationTests.Helpers;
public static class Utilities
{
    public static void InitializeDatabase(InvoiceDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        SeedDatabase(context);
    }

    public static void Cleanup(InvoiceDbContext context)
    {
        context.Contacts.ExecuteDelete();
        context.Invoices.ExecuteDelete();
        context.SaveChanges();
        SeedDatabase(context);
    }

    private static void SeedDatabase(InvoiceDbContext context)
    {
        // Create a few Contacts
        var contacts = new List<Contact>
        {
            new()
            {
                Id = Guid.Parse("8a9de219-2dde-4f2a-9ebd-b1f8df9fef03"),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            },
            new()
            {
                Id = Guid.Parse("798a4706-4c51-48c9-8310-531d7364c926"),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com"
            }
        };
        context.Contacts.AddRange(contacts);
        // Create a few Invoices
        var invoices = new List<Invoice>
        {
            new()
            {
                Id = Guid.Parse("7e096984-5919-492c-8d4f-ce93f25eaed5"),
                InvoiceNumber = "INV-001",
                Amount = 500,
                DueDate = DateTimeOffset.Now.AddDays(30),
                Contact = contacts[0],
                Status = InvoiceStatus.AwaitPayment,
                InvoiceDate = DateTimeOffset.Now,
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        Id = Guid.Parse("cf99ae58-2509-4f63-9e47-08b722f8bfb5"),
                        Description = "Item 1",
                        Quantity = 1,
                        UnitPrice = 100,
                        Amount = 100
                    },
                    new()
                    {
                        Id = Guid.Parse("0101d6e6-9634-4cf5-9daa-d09d936e8d58"),
                        Description = "Item 2",
                        Quantity = 2,
                        UnitPrice = 200,
                        Amount = 400
                    }
                }
            },
            new()
            {
                Id = Guid.Parse("b1ca459c-6874-4f2b-bc9d-f3a45a9120e4"),
                InvoiceNumber = "INV-002",
                Amount = 1000,
                DueDate = DateTimeOffset.Now.AddDays(30),
                Contact = contacts[1],
                Status = InvoiceStatus.Draft,
                InvoiceDate = DateTimeOffset.Now,
                InvoiceItems = new List<InvoiceItem>
                {
                    new()
                    {
                        Id = Guid.Parse("8fd2fe94-0cb3-491d-92e4-fd26811462c3"),
                        Description = "Item 1",
                        Quantity = 2,
                        UnitPrice = 100,
                        Amount = 200
                    },
                    new()
                    {
                        Id = Guid.Parse("6f493d9b-cb53-4c86-9f70-958936372902"),
                        Description = "Item 2",
                        Quantity = 4,
                        UnitPrice = 200,
                        Amount = 800
                    }
                }
            }
        };
        context.Invoices.AddRange(invoices);
        context.SaveChanges();
    }

}
