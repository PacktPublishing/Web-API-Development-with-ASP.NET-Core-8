using FluentAssertions;
using InvoiceApp.IntegrationTests.Helpers;
using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;

namespace InvoiceApp.IntegrationTests;

[Collection("CustomIntegrationTests")]
public class ContactsApiTestsWithCollection : IDisposable
{
    private readonly CustomIntegrationTestsFixture _factory;

    public ContactsApiTestsWithCollection(CustomIntegrationTestsFixture factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetContacts_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/contact");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var contacts = JsonSerializer.Deserialize<List<Contact>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        contacts.Should().NotBeNull();
        contacts.Should().HaveCount(2);
    }

    [Theory]
    [InlineData("8a9de219-2dde-4f2a-9ebd-b1f8df9fef03")]
    [InlineData("798a4706-4c51-48c9-8310-531d7364c926")]
    public async Task GetContactById_ReturnsSuccessAndCorrectContentType(string id)
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync($"/api/contact/{id}");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var contact = JsonSerializer.Deserialize<Contact>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        contact.Should().NotBeNull();
        contact!.Id.Should().Be(id);
    }

    [Fact]
    public async Task GetContactById_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync($"/api/contact/{Guid.NewGuid()}");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public void Dispose()
    {
        var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<InvoiceDbContext>();
        Utilities.Cleanup(db);
    }
}
