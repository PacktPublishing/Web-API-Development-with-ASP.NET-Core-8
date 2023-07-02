using InvoiceApp.WebApi.Controllers;
using InvoiceApp.WebApi.Interfaces;
using InvoiceApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InvoiceApp.UnitTests;
public class ContactControllerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public ContactControllerTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetContacts_ShouldReturnContacts()
    {
        // Arrange
        var contactsRepositoryMock = new Mock<IContactRepository>();
        contactsRepositoryMock.Setup(x => x.GetContactsAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((int page, int pageSize) =>
                _fixture.Contacts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList());
        var invoiceRepositoryMock = new Mock<IInvoiceRepository>();

        var controller = new ContactController(contactsRepositoryMock.Object, invoiceRepositoryMock.Object);
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
