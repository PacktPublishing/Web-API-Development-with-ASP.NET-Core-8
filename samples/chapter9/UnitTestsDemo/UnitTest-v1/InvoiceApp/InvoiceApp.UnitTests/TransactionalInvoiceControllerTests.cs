using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Controllers;
using InvoiceApp.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace InvoiceApp.UnitTests;

[Collection("TransactionalTests")]
public class TransactionalInvoiceControllerTests(TransactionalTestDatabaseFixture fixture) : IDisposable
{
    [Fact]
    public async Task UpdateInvoiceStatusAsync_ShouldUpdateStatus()
    {
        // Arrange
        await using var dbContext = fixture.CreateDbContext();
        var emailServiceMock = new Mock<IEmailService>();
        var controller = new InvoiceController(dbContext, emailServiceMock.Object);
        // Act
        var invoice = await dbContext.Invoices.FirstAsync(x => x.Status == InvoiceStatus.AwaitPayment);
        await controller.UpdateInvoiceStatusAsync(invoice.Id, InvoiceStatus.Paid);
        // Assert
        dbContext.ChangeTracker.Clear();
        var updatedInvoice = await dbContext.Invoices.FindAsync(invoice.Id);
        Assert.NotNull(updatedInvoice);
        Assert.Equal(InvoiceStatus.Paid, updatedInvoice.Status);
    }

    public void Dispose()
    {
        fixture.Cleanup();
    }
}
