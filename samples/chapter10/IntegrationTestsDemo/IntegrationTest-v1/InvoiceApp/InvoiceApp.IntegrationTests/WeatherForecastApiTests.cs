using FluentAssertions;
using InvoiceApp.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace InvoiceApp.IntegrationTests;
public class WeatherForecastApiTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync("/WeatherForecast");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var weatherForecast = JsonSerializer.Deserialize<List<WeatherForecast>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        weatherForecast.Should().NotBeNull();
        weatherForecast.Should().HaveCount(5);
    }
}
