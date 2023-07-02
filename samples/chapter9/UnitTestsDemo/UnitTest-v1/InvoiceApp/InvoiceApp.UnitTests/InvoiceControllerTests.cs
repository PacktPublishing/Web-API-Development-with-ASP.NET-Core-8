using FluentAssertions;
using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Controllers;
using InvoiceApp.WebApi.Models;
using InvoiceApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InvoiceApp.UnitTests;
public class InvoiceControllerTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public InvoiceControllerTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetInvoices_ShouldReturnInvoices()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var emailServiceMock = new Mock<IEmailService>();
        var controller = new InvoiceController(dbContext, emailServiceMock.Object);
        // Act
        var actionResult = await controller.GetInvoicesAsync();
        // Assert
        var result = actionResult.Result as OkObjectResult;
        Assert.NotNull(result);
        var returnResult = Assert.IsAssignableFrom<List<Invoice>>(result.Value);
        //Assert.NotNull(returnResult);
        //Assert.Equal(2, returnResult.Count);
        //Assert.Contains(returnResult, i => i.InvoiceNumber == "INV-001");
        //Assert.Contains(returnResult, i => i.InvoiceNumber == "INV-002");
        returnResult.Should().NotBeNull();
        //returnResult.Should().HaveCount(2);
        returnResult.Count.Should().Be(2, "The number of invoices should be 2");
        returnResult.Should().Contain(i => i.InvoiceNumber == "INV-001");
        returnResult.Should().Contain(i => i.InvoiceNumber == "INV-002");
    }

    [Theory]
    [InlineData(InvoiceStatus.AwaitPayment)]
    [InlineData(InvoiceStatus.Draft)]
    public async Task GetInvoicesByStatus_ShouldReturnInvoices(InvoiceStatus status)
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var emailServiceMock = new Mock<IEmailService>();
        var controller = new InvoiceController(dbContext, emailServiceMock.Object);
        // Act
        var actionResult = await controller.GetInvoicesAsync(status: status);
        // Assert
        var result = actionResult.Result as OkObjectResult;
        Assert.NotNull(result);
        var returnResult = Assert.IsAssignableFrom<List<Invoice>>(result.Value);
        Assert.NotNull(returnResult);
        Assert.Single(returnResult);
        Assert.Equal(status, returnResult.First().Status);
    }

    [Fact]
    public async Task CreateInvoice_ShouldCreateInvoice()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var emailServiceMock = new Mock<IEmailService>();
        var controller = new InvoiceController(dbContext, emailServiceMock.Object);
        // Act
        var contactId = dbContext.Contacts.First().Id;
        var invoice = new Invoice
        {
            DueDate = DateTimeOffset.Now.AddDays(30),
            ContactId = contactId,
            Status = InvoiceStatus.Draft,
            InvoiceDate = DateTimeOffset.Now,
            InvoiceItems = new List<InvoiceItem>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "Item 1",
                    Quantity = 1,
                    UnitPrice = 100,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Description = "Item 2",
                    Quantity = 2,
                    UnitPrice = 200,
                }
            }
        };
        var actionResult = await controller.CreateInvoiceAsync(invoice);
        // Assert
        var result = actionResult.Result as CreatedAtActionResult;
        Assert.NotNull(result);
        var returnResult = Assert.IsAssignableFrom<Invoice>(result.Value);
        var invoiceCreated = await dbContext.Invoices.FindAsync(returnResult.Id);

        Assert.NotNull(invoiceCreated);
        Assert.Equal(InvoiceStatus.Draft, invoiceCreated.Status);
        Assert.Equal(500, invoiceCreated.Amount);
        Assert.Equal(3, dbContext.Invoices.Count());
        Assert.Equal(contactId, invoiceCreated.ContactId);
        // You can add more assertions here

        // Clean up
        dbContext.Invoices.Remove(invoiceCreated);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task UpdateInvoice_ShouldUpdateInvoice()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var emailServiceMock = new Mock<IEmailService>();
        var controller = new InvoiceController(dbContext, emailServiceMock.Object);
        // Act
        // Start a transaction to prevent the changes from being saved to the database
        await dbContext.Database.BeginTransactionAsync();
        var invoice = dbContext.Invoices.First();
        invoice.Status = InvoiceStatus.Paid;
        invoice.Description = "Updated description";
        invoice.InvoiceItems.ForEach(x =>
        {
            x.Description = "Updated description";
            x.UnitPrice += 100;
        });
        var expectedAmount = invoice.InvoiceItems.Sum(x => x.UnitPrice * x.Quantity);
        await controller.UpdateInvoiceAsync(invoice.Id, invoice);

        // Assert
        dbContext.ChangeTracker.Clear();
        var invoiceUpdated = await dbContext.Invoices.SingleAsync(x => x.Id == invoice.Id);
        //Assert.Equal(InvoiceStatus.Paid, invoiceUpdated.Status);
        //Assert.Equal("Updated description", invoiceUpdated.Description);
        //Assert.Equal(expectedAmount, invoiceUpdated.Amount);
        //Assert.Equal(2, dbContext.Invoices.Count());
        invoiceUpdated.Status.Should().Be(InvoiceStatus.Paid);
        invoiceUpdated.Description.Should().Be("Updated description");
        invoiceUpdated.Amount.Should().Be(expectedAmount);
        invoiceUpdated.InvoiceItems.Should().HaveCount(2);
    }
}
