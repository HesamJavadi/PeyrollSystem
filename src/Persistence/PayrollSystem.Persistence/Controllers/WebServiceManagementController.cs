using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Service.Management.WebServiceManagement;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebServiceManagementController : ControllerBase
    {

        protected readonly IWebServiceManagementService _service;

        public WebServiceManagementController(IWebServiceManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<WebServiceManagementDto> Get()
        {
            return _service.GetAll();
        }

        // POST api/<WebServiceManagementController>
        [HttpPost]
        public void Post([FromBody] WebServiceManagementDto value)
        {
            _service.Create(value);
        }


    }
}
