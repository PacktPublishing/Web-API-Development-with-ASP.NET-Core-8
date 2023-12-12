using CqrsDemo.Core;
using CqrsDemo.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Invoice> Invoices => Set<Invoice>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<Invoice>().HasData(
            new Invoice
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                InvoiceNumber = "INV-0001",
                ContactName = "John Doe",
                Description = "Invoice 1",
                Amount = 2000,
                InvoiceDate = DateTimeOffset.UtcNow.AddDays(-10),
                DueDate = DateTimeOffset.UtcNow.AddDays(20),
                Status = InvoiceStatus.Draft
            },
            new Invoice
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                InvoiceNumber = "INV-0002",
                ContactName = "Jane Doe",
                Description = "Invoice 2",
                Amount = 2000,
                InvoiceDate = DateTimeOffset.UtcNow.AddDays(-5),
                DueDate = DateTimeOffset.UtcNow.AddDays(15),
                Status = InvoiceStatus.Draft
            },
            new Invoice
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                InvoiceNumber = "INV-0003",
                ContactName = "John Doe",
                Description = "Invoice 3",
                Amount = 3500,
                InvoiceDate = DateTimeOffset.UtcNow.AddDays(-2),
                DueDate = DateTimeOffset.UtcNow.AddDays(10),
                Status = InvoiceStatus.Draft
            },
            new Invoice
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                InvoiceNumber = "INV-0004",
                ContactName = "Jane Doe",
                Description = "Invoice 4",
                Amount = 5500,
                InvoiceDate = DateTimeOffset.UtcNow.AddDays(-1),
                DueDate = DateTimeOffset.UtcNow.AddDays(5),
                Status = InvoiceStatus.Draft
            },
            new Invoice
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                InvoiceNumber = "INV-0005",
                ContactName = "John Doe",
                Description = "Invoice 5",
                Amount = 8000,
                InvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(2),
                Status = InvoiceStatus.Draft
            }
        );
        modelBuilder.Entity<InvoiceItem>().HasData(
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Description = "Item 1",
                Quantity = 1,
                UnitPrice = 1000,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Description = "Item 2",
                Quantity = 2,
                UnitPrice = 500,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                Description = "Item 1",
                Quantity = 1,
                UnitPrice = 1000,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                Description = "Item 2",
                Quantity = 2,
                UnitPrice = 500,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                Description = "Item 1",
                Quantity = 1,
                UnitPrice = 1000,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                Description = "Item 2",
                Quantity = 2,
                UnitPrice = 500,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Description = "Item 3",
                Quantity = 3,
                UnitPrice = 500,
                Amount = 1500,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                Description = "Item 1",
                Quantity = 1,
                UnitPrice = 1000,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                Description = "Item 2",
                Quantity = 2,
                UnitPrice = 500,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                Description = "Item 3",
                Quantity = 3,
                UnitPrice = 500,
                Amount = 1500,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                Description = "Item 4",
                Quantity = 4,
                UnitPrice = 500,
                Amount = 2000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000051"),
                Description = "Item 1",
                Quantity = 1,
                UnitPrice = 1000,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000052"),
                Description = "Item 2",
                Quantity = 2,
                UnitPrice = 500,
                Amount = 1000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000053"),
                Description = "Item 3",
                Quantity = 3,
                UnitPrice = 500,
                Amount = 1500,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000054"),
                Description = "Item 4",
                Quantity = 4,
                UnitPrice = 500,
                Amount = 2000,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new InvoiceItem
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000055"),
                Description = "Item 5",
                Quantity = 5,
                UnitPrice = 500,
                Amount = 2500,
                InvoiceId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            }
        );
    }
}
