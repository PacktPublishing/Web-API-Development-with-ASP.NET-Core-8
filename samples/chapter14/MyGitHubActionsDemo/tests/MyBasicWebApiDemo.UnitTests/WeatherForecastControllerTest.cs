using Microsoft.Extensions.Logging;
using Moq;
using MyBasicWebApiDemo.Controllers;

namespace MyBasicWebApiDemo.UnitTests;

public class WeatherForecastControllerTest
{
    [Fact]
    public void Get_ReturnsWeatherForecast()
    {
        // Arrange
        var logger = Mock.Of<ILogger<WeatherForecastController>>();
        var controller = new WeatherForecastController(logger);

        // Act
        var result = controller.Get();

        // Assert
        var weatherForecast = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);
        Assert.Equal(5, weatherForecast.Count());
    }

}