using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClaimBasedAuthorizationDemo.IntegrationTests;
public class AuthTests(IntegrationTestsFixture fixture) : IClassFixture<IntegrationTestsFixture>
{
    [Fact]
    public async Task GetAnonymousWeatherForecast_ShouldReturnOk()
    {
        // Arrange
        var client = fixture.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast/anonymous");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var weatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        weatherForecasts.Should().NotBeNull();
        weatherForecasts.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetWeatherForecast_ShouldReturnUnauthorized_WhenNotAuthorized()
    {
        // Arrange
        var client = fixture.CreateClient();
        // Act
        var response = await client.GetAsync("/weatherforecast");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetWeatherForecast_ShouldReturnOk_WhenAuthorized()
    {
        // Arrange
        var token = fixture.GenerateToken("TestUser");
        var client = fixture.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        // Act
        var response = await client.GetAsync("/weatherforecast");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
        // Deserialize the response
        var responseContent = await response.Content.ReadAsStringAsync();
        var weatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        weatherForecasts.Should().NotBeNull();
        weatherForecasts.Should().HaveCount(5);
    }

    // Use the TestAuthHandler to create integration tests
    [Fact]
    public async Task GetDrivingLicense_ShouldReturnUnauthorized_WhenNotAuthorized()
    {
        // Arrange
        var client = fixture.CreateClient();
        // Act
        var response = await client.GetAsync("/weatherforecast/driving-license");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetDrivingLicense_ShouldReturnOk_WhenAuthorizedWithTestAuthHandler()
    {
        // Arrange
        var client = fixture.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                //services.Configure<TestAuthHandlerOptions>(options =>
                //{
                //    options.UserName = "Test User";
                //});
                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuthHandler.AuthenticationScheme;
                        options.DefaultChallengeScheme = TestAuthHandler.AuthenticationScheme;
                        options.DefaultScheme = TestAuthHandler.AuthenticationScheme;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme,
                        options => { });
            });
        }).CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add(TestAuthHandler.UserNameHeader, "Test User");
        client.DefaultRequestHeaders.Add(TestAuthHandler.CountryHeader, "New Zealand");
        client.DefaultRequestHeaders.Add(TestAuthHandler.AccessNumberHeader, "123456");
        client.DefaultRequestHeaders.Add(TestAuthHandler.DrivingLicenseNumberHeader, "12345678");
        // Act
        var response = await client.GetAsync("/weatherforecast/driving-license");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
    }

    [Fact]
    public async Task GetDrivingLicense_ShouldReturnForbidden_WhenRequiredClaimsNotProvidedWithTestAuthHandler()
    {
        // Arrange
        var client = fixture.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuthHandler.AuthenticationScheme;
                        options.DefaultChallengeScheme = TestAuthHandler.AuthenticationScheme;
                        options.DefaultScheme = TestAuthHandler.AuthenticationScheme;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme,
                        options => { });
            });
        }).CreateClient();
        //var client = _fixture.CreateClientWithAuth("Test User", "New Zealand", "123456", "12345678");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add(TestAuthHandler.UserNameHeader, "Test User");
        client.DefaultRequestHeaders.Add(TestAuthHandler.CountryHeader, "New Zealand");
        client.DefaultRequestHeaders.Add(TestAuthHandler.AccessNumberHeader, "123456");
        // Act
        var response = await client.GetAsync("/weatherforecast/driving-license");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetCountry_ShouldReturnUnauthorized_WhenNotAuthorized()
    {
        // Arrange
        var client = fixture.CreateClient();
        // Act
        var response = await client.GetAsync("/weatherforecast/country");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetCountry_ShouldReturnOk_WhenAuthorizedWithTestAuthHandler()
    {
        // Arrange
        var client = fixture.CreateClientWithAuth("Test User", "New Zealand", "123456", "12345678");
        // Act
        var response = await client.GetAsync("/weatherforecast/country");
        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.ToString().Should().Be("application/json; charset=utf-8");
    }

    [Fact]
    public async Task GetCountry_ShouldReturnForbidden_WhenRequiredClaimsNotProvidedWithTestAuthHandler()
    {
        // Arrange
        // As we don't provide the country claim, the request will be forbidden
        var client = fixture.CreateClientWithAuth("Test User", "", "123456", "12345678");
        // Act
        var response = await client.GetAsync("/weatherforecast/country");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

}
