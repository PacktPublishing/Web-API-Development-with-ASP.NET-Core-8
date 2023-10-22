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
        [Route("message-args")]
        public ActionResult MessageArgsSample()
        {
            _logger.LogInformation("This is a logging message with args: Today is {Week}. It is {Time}.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
            return Ok("This is to test the message with arguments.");
        }

        [HttpGet]
        [Route("structured-logging")]
        public ActionResult StructuredLoggingSample()
        {
            _logger.LogInformation("This is a logging message with args: Today is {Week}. It is {Time}.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
            _logger.LogInformation($"This is a logging message with string concatenation: Today is {DateTime.Now.DayOfWeek}. It is {DateTime.Now.ToLongTimeString()}.");
            return Ok("This is to test the difference between structured logging and string concatenation.");
        }

        [HttpGet]
        [Route("exception")]
        public ActionResult ExceptionSample()
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
                _logger.LogError(ex, "This is an exception logging message. Datetime: {exceptionDateTime}. Exception message: {exceptionMessage}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message);
                // throw;
            }
            return Ok("This is to test an exception.");
        }
    }
}
