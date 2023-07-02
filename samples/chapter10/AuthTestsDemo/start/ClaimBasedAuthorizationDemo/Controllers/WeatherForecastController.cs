using ClaimBasedAuthorizationDemo.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClaimBasedAuthorizationDemo.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [Authorize(Policy = AppAuthorizationPolicies.RequireDrivingLicense)]
    [HttpGet("driving-license")]
    public IActionResult GetDrivingLicense()
    {
        var drivingLicenseNumber = User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.DrivingLicenseNumber)?.Value;
        return Ok(new { drivingLicenseNumber });
    }

    [Authorize(Policy = AppAuthorizationPolicies.RequireCountry)]
    [HttpGet("country")]
    public IActionResult GetCountry()
    {
        var country = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country)?.Value;
        return Ok(new { country });
    }

    [Authorize(Policy = AppAuthorizationPolicies.RequireDrivingLicenseAndAccessNumber)]
    [HttpGet("driving-license-and-access-number")]
    public IActionResult GetDrivingLicenseAndAccessNumber()
    {
        var drivingLicenseNumber = User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.DrivingLicenseNumber)?.Value;
        var accessNumber = User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.AccessNumber)?.Value;
        return Ok(new { drivingLicenseNumber, accessNumber });
    }

    //[Authorize(Policy = AppAuthorizationPolicies.RequireDrivingLicense)]
    //[Authorize(Policy = AppAuthorizationPolicies.RequireAccessNumber)]
    //[HttpGet("driving-license-and-access-number")]
    //public IActionResult GetDrivingLicenseAndAccessNumber()
    //{
    //    var drivingLicenseNumber = User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.DrivingLicenseNumber)?.Value;
    //    var accessNumber = User.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.AccessNumber)?.Value;
    //    return Ok(new { drivingLicenseNumber, accessNumber });
    //}

}
