using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly IDemoService _demoService;

    public DemoController(IDemoService demoService)
    {
        _demoService = demoService;
    }

    [HttpGet]
    public ActionResult Get()
    {
        return Content(_demoService.SayHello());
    }
}

