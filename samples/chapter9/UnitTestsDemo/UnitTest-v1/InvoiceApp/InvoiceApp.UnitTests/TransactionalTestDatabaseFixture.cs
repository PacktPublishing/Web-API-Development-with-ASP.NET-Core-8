using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.UnitTests;
public class TransactionalTestDatabaseFixture
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=InvoiceTransactionalTestDb;Trusted_Connection=True";
    public TransactionalTestDatabaseFixture()
    {
        // This code comes from Microsoft Docs: https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Testing/TestingWithTheDatabase/TransactionalTestDatabaseFixture.cs
        using var context = CreateDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        InitializeDatabase();
    }

    public InvoiceDbContext CreateDbContext()
        => new(new DbContextOptionsBuilder<InvoiceDbContext>()
            .UseSqlServer(ConnectionString)
            .Options, null);

    public void InitializeDatabase()
    {
        using var context = CreateDbContext();
        // Create a few Contacts
        var contacts = new List<Contact>
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
        context.Contacts.AddRange(contacts);
        // Create a few Invoices
        var invoices = new List<Invoice>
        {
            new()
            {
                Id = Guid.NewGuid(),
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
                Contact = contacts[1],
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
        context.Invoices.AddRange(invoices);
        context.SaveChanges();
    }

    public void Cleanup()
    {
        using var context = CreateDbContext();
        context.Contacts.ExecuteDelete();
        context.Invoices.ExecuteDelete();
        context.SaveChanges();
        InitializeDatabase();
    }
}
