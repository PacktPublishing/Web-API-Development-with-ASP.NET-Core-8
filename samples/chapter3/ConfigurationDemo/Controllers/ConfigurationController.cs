using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ConfigurationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    [Route("my-key")]
    public ActionResult GetMyKey()
    {
        var myKey = _configuration["MyKey"];
        return Ok(myKey);
    }

    [HttpGet]
    [Route("database-configuration")]
    public ActionResult GetDatabaseConfiguration()
    {
        var type = _configuration["database:Type"];
        var connectionString = _configuration["Database:ConnectionString"];
        return Ok(new { Type = type, ConnectionString = connectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-bind")]
    public ActionResult GetDatabaseConfigurationWithBind()
    {
        var databaseOption = new DatabaseOption();
        // The `SectionName` is defined in the `DatabaseOption` class, which shows the section name in the `appsettings.json` file.
        _configuration.GetSection(DatabaseOption.SectionName).Bind(databaseOption);
        // You can also use the code below to achieve the same result
        // _configuration.Bind(DatabaseOption.SectionName, databaseOption);
        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-generic-type")]
    public ActionResult GetDatabaseConfigurationWithGenericType()
    {
        var databaseOption = _configuration.GetSection(DatabaseOption.SectionName).Get<DatabaseOption>();
        return Ok(new { databaseOption?.Type, databaseOption?.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-ioptions")]
    public ActionResult GetDatabaseConfigurationWithIOptions([FromServices] IOptions<DatabaseOption> options)
    {
        var databaseOption = options.Value;
        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-ioptions-snapshot")]
    public ActionResult GetDatabaseConfigurationWithIOptionsSnapshot([FromServices] IOptionsSnapshot<DatabaseOption> options)
    {
        var databaseOption = options.Value;
        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-ioptions-monitor")]
    public ActionResult GetDatabaseConfigurationWithIOptionsMonitor([FromServices] IOptionsMonitor<DatabaseOption> options)
    {
        var databaseOption = options.CurrentValue;
        return Ok(new { databaseOption.Type, databaseOption.ConnectionString });
    }

    [HttpGet]
    [Route("database-configuration-with-named-options")]
    public ActionResult GetDatabaseConfigurationWithNamedOptions([FromServices] IOptionsSnapshot<DatabaseOptions> options)
    {
        var systemDatabaseOption = options.Get(DatabaseOptions.SystemDatabaseSectionName);
        var businessDatabaseOption = options.Get(DatabaseOptions.BusinessDatabaseSectionName);
        return Ok(new { SystemDatabaseOption = systemDatabaseOption, BusinessDatabaseOption = businessDatabaseOption });
    }
}
