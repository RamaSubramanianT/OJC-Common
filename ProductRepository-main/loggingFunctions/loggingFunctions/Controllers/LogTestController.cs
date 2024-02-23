using Microsoft.AspNetCore.Mvc;
using log4net;
namespace loggingFunctions.Controllers
{
    
    public class LogTestController : ControllerBase
    {
        private readonly ILog _logger;
        public LogTestController(ILog logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public IActionResult Get()
        {
            _logger.Info("This is an informational log.");
            _logger.Error("This is an error log.");
            return Ok("Success");
        }
    }
}
