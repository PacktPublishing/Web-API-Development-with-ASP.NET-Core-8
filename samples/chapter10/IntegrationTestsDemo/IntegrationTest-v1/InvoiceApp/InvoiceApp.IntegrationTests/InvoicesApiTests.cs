using FluentAssertions;
using InvoiceApp.IntegrationTests.Helpers;
using InvoiceApp.WebApi;
using InvoiceApp.WebApi.Data;
using InvoiceApp.WebApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;

namespace InvoiceApp.IntegrationTests;

public class InvoicesApiTests(CustomIntegrationTestsFixture factory) : IClassFixture<CustomIntegrationTestsFixture>
{
    [Fact(Skip = "Skipped")]
    public async Task GetInvoices_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync("/api/invoice");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var invoices = JsonSerializer.Deserialize<List<Invoice>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        invoices.Should().NotBeNull();
        invoices.Should().HaveCount(2);
    }

    [Theory(Skip = "Skipped")]
    [InlineData("7e096984-5919-492c-8d4f-ce93f25eaed5")]
    [InlineData("b1ca459c-6874-4f2b-bc9d-f3a45a9120e4")]
    public async Task GetInvoiceById_ReturnsSuccessAndCorrectContentType(string id)
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync($"/api/invoice/{id}");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var invoice = JsonSerializer.Deserialize<Invoice>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        invoice.Should().NotBeNull();
        invoice!.Id.Should().Be(id);
    }

    [Fact(Skip = "Skipped")]
    public async Task GetInvoiceById_ReturnsNotFound()
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync($"/api/invoice/{Guid.NewGuid()}");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact(Skip = "Skipped")]
    public async Task PostInvoice_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = factory.CreateClient();
        var invoice = new Invoice
        {
            DueDate = DateTimeOffset.Now.AddDays(30),
            ContactId = Guid.Parse("8a9de219-2dde-4f2a-9ebd-b1f8df9fef03"),
            Status = InvoiceStatus.Draft,
            InvoiceItems = new List<InvoiceItem>
            {
                new()
                {
                    Id = Guid.Parse("d38b3fbb-c31a-4176-a100-26529519045e"),
                    Description = "Item 1",
                    Quantity = 1,
                    UnitPrice = 100,
                },
                new()
                {
                    Id = Guid.Parse("2803187e-a093-4147-b554-bff800fcb80c"),
                    Description = "Item 2",
                    Quantity = 2,
                    UnitPrice = 200,
                }
            }
        };
        var json = JsonSerializer.Serialize(invoice);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/invoice", data);
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var invoiceResponse = JsonSerializer.Deserialize<Invoice>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        invoiceResponse.Should().NotBeNull();
        invoiceResponse!.Id.Should().NotBeEmpty();
        invoiceResponse.Amount.Should().Be(500);
        invoiceResponse.Status.Should().Be(invoice.Status);
        invoiceResponse.ContactId.Should().Be(invoice.ContactId);

        // Clean up the database
        var scope = factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<InvoiceDbContext>();
        Utilities.Cleanup(db);
    }

    [Fact(Skip = "Skipped")]
    public async Task PostInvoice_WhenContactIdDoesNotExist_ReturnsBadRequest()
    {
        // Arrange
        var client = factory.CreateClient();
        var invoice = new Invoice
        {
            DueDate = DateTimeOffset.Now.AddDays(30),
            ContactId = Guid.NewGuid(),
            Status = InvoiceStatus.Draft,
            InvoiceItems = new List<InvoiceItem>
            {
                new()
                {
                    Id = Guid.Parse("d38b3fbb-c31a-4176-a100-26529519045e"),
                    Description = "Item 1",
                    Quantity = 1,
                    UnitPrice = 100,
                },
                new()
                {
                    Id = Guid.Parse("2803187e-a093-4147-b554-bff800fcb80c"),
                    Description = "Item 2",
                    Quantity = 2,
                    UnitPrice = 200,
                }
            }
        };
        var json = JsonSerializer.Serialize(invoice);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/invoice", data);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
