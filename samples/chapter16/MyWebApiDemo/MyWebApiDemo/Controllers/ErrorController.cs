using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApiDemo.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger) =>
        _logger = logger;

    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        _logger.LogError(exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

    [Route("/error")]
    public IActionResult HandleError()
    {
        var exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        _logger.LogError(exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
        return Problem();
    }
}
