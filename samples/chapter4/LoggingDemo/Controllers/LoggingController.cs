using Microsoft.AspNetCore.Mvc;

namespace LoggingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController(ILogger<LoggingController> logger) : ControllerBase
    {
        [HttpGet]
        [Route("message-args")]
        public ActionResult MessageArgsSample()
        {
            logger.LogInformation("This is a logging message with args: Today is {Week}. It is {Time}.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
            return Ok("This is to test the message with arguments.");
        }

        [HttpGet]
        [Route("structured-logging")]
        public ActionResult StructuredLoggingSample()
        {
            logger.LogInformation("This is a logging message with args: Today is {Week}. It is {Time}.", DateTime.Now.DayOfWeek, DateTime.Now.ToLongTimeString());
            logger.LogInformation($"This is a logging message with string concatenation: Today is {DateTime.Now.DayOfWeek}. It is {DateTime.Now.ToLongTimeString()}.");
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
                logger.LogError(ex, "This is an exception logging message. Datetime: {exceptionDateTime}. Exception message: {exceptionMessage}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message);
                // throw;
            }
            return Ok("This is to test an exception.");
        }
    }
}
