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

        // GET api/<WebServiceManagementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WebServiceManagementController>
        [HttpPost]
        public void Post([FromBody] WebServiceManagementDto value)
        {
            _service.Create(value);
        }

        // PUT api/<WebServiceManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WebServiceManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
