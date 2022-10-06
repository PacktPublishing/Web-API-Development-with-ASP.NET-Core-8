using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;
        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("exception")]
        public StringContent ExceptionSample()
        {
            try
            {
                var random = new Random();
                var randomNumber = random.Next(1, 6);
                if (randomNumber == 3)
                {
                    throw new Exception("This is a generic exception");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "This is an exception logging message. Datetime: {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                // throw;
            }
            return new StringContent("This is to test an exception.");
        }
    }
}
