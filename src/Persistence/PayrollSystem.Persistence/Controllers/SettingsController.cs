using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PayrollSystem.Domain.Contracts.Dtos.Management.Setting;
using PayrollSystem.Domain.Contracts.Request.Setting;
using PayrollSystem.Domain.Contracts.Service.Management.Setting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {

        private readonly ISettingService _service;

        public SettingsController(ISettingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<SettingDto> Get()
        {
            return _service.GetAll();
        }


        [HttpGet]
        public SettingDto GetDefaultSetting()
        {
            return _service.GetDefaultSetting();
        }

        [HttpGet("{id}")]
        public SettingDto Get(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] SettingRequest value)
        {
            _service.Create(value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] SettingRequest request)
        {
           
            var result = await _service.UpdateAsync(id,request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
