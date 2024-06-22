using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Domain.Contracts.Service.Logger;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loggerController : ControllerBase
    {
        private readonly ISerilogLoggerService _loggerService;

        public loggerController(ISerilogLoggerService loggerService)
        {
            _loggerService = loggerService; 
        }
        [HttpGet]
        public IActionResult GetLogs()
        {
           var data = _loggerService.GetLogs(40,40);
            return Ok(data);
        }
    }
}
