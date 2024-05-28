using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Request.PayStatement;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayStatementController : ControllerBase
    {
        private readonly IPayStatementService _service;
        public PayStatementController(IPayStatementService service)
        {
            this._service = service;
        }

        [HttpGet]
        public List<PayStatementDto> GetNumber([FromQuery] GetPayStatementRequest PayStatement)
        {
            return _service.GetPayStatementNumber(PayStatement);
        }

        [HttpGet]
        public List<PayStatementDetailDto> Get([FromQuery] GetPayStatementRequest PayStatement)
        {
            return _service.GetPayStatementDetail(PayStatement);
        }

    }
}
