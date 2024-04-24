using Microsoft.AspNetCore.Mvc;

namespace EnvironmentsDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    [Route("database-configuration")]
    public ActionResult GetDatabaseConfiguration()
    {
        var type = configuration["database:Type"];
        var connectionString = configuration["Database:ConnectionString"];
        return Ok(new { Type = type, ConnectionString = connectionString });
    }
}
