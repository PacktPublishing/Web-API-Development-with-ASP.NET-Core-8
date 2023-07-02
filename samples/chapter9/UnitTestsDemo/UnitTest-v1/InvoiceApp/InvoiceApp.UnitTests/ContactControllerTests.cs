using InvoiceApp.WebApi.Controllers;
using InvoiceApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.UnitTests;
public class ContactControllerTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public ContactControllerTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetContacts_ShouldReturnContacts()
    {
        // Arrange
        await using var dbContext = _fixture.CreateDbContext();
        var controller = new ContactController(dbContext);
        // Act
        var actionResult = await controller.GetContactsAsync();
        // Assert
        var result = actionResult.Result as OkObjectResult;
        Assert.NotNull(result);
        var returnResult = Assert.IsAssignableFrom<List<Contact>>(result.Value);
        Assert.NotNull(returnResult);
        Assert.Equal(2, returnResult.Count);
        Assert.Contains(returnResult, c => c.FirstName == "John");
        Assert.Contains(returnResult, c => c.FirstName == "Jane");
    }
}
