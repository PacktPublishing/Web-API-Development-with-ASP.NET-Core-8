using EfCoreRelationshipsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipsDemo.Data
{
    public static class SeedDataExtensions
    {
        public static void SeedInvoiceData(this ModelBuilder builder)
        {
            builder.Entity<Invoice>().HasData(
                new Invoice
                {
                    Id = Guid.NewGuid(),
                    InvoiceNumber = "INV-001",
                    ContactName = "Iron Man",
                    Description = "Invoice for the first month",
                    Amount = 100,
                    InvoiceDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    DueDate = new DateTimeOffset(2023, 1, 15, 0, 0, 0, TimeSpan.Zero),
                    Status = InvoiceStatus.AwaitPayment
                },
                new Invoice
                {
                    Id = Guid.NewGuid(),
                    InvoiceNumber = "INV-002",
                    ContactName = "Captain America",
                    Description = "Invoice for the first month",
                    Amount = 200,
                    InvoiceDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    DueDate = new DateTimeOffset(2023, 1, 15, 0, 0, 0, TimeSpan.Zero),
                    Status = InvoiceStatus.AwaitPayment
                },
                new Invoice
                {
                    Id = Guid.NewGuid(),
                    InvoiceNumber = "INV-003",
                    ContactName = "Thor",
                    Description = "Invoice for the first month",
                    Amount = 300,
                    InvoiceDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    DueDate = new DateTimeOffset(2023, 1, 15, 0, 0, 0, TimeSpan.Zero),
                    Status = InvoiceStatus.Draft
                });
            //builder.Entity<Contact>().HasData(
            //    new Contact
            //    {
            //        Id = Guid.NewGuid(),
            //        FirstName = "Tony",
            //        LastName = "Stark",
            //        Email = "tony.stark@avengers.com",
            //        Phone = "1234567890",
            //        Address = new Address()
            //        {
            //            City = "New York",
            //            Country = "USA",
            //            Street = "Stark Tower",
            //            ZipCode = "12345",
            //            State = "NY",
            //            ContactId = 
            //        }
            //    },
            //    new Contact
            //    {
            //        Id = Guid.NewGuid(),
            //        FirstName = "Steve",
            //        LastName = "Rogers",
            //        Email = "steve.rogers@avengers.com",
            //        Phone = "1234567890",
            //        Address = new Address()
            //        {
            //            City = "New York",
            //            Country = "USA",
            //            Street = "Avengers Tower",
            //            ZipCode = "12345",
            //            State = "NY"
            //        }
            //    });
        }
    }
}
