﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayStubController : ControllerBase
    {
        private readonly IPayStubService _service;
        public PayStubController(IPayStubService service)
        {
            this._service = service;
        }

        [HttpGet]
        public PayStubDto Get()
        {
            return _service.GetPayStub();
        }

    }
}
