using InvoiceApp.WebApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.UnitTests;

[Collection("TransactionalTests")]
public class TransactionalContactControllerTests : IDisposable
{
    private readonly TransactionalTestDatabaseFixture _fixture;

    public TransactionalContactControllerTests(TransactionalTestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task UpdateContactAsync_ShouldUpdateContact()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var controller = new ContactController(dbContext);
        // Act
        var contact = await dbContext.Contacts.FirstAsync(x => x.FirstName == "John");
        contact.FirstName = "Johnathan";
        await controller.UpdateContactAsync(contact.Id, contact);
        // Assert
        dbContext.ChangeTracker.Clear();
        var updatedContact = await dbContext.Contacts.FindAsync(contact.Id);
        Assert.NotNull(updatedContact);
        Assert.Equal("Johnathan", updatedContact.FirstName);
    }

    public void Dispose()
    {
        _fixture.Cleanup();
    }
}
