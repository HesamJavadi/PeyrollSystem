using AutoMapper;
using Microsoft.AspNet.Identity;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Personnel.PayStub
{
    public class PayStubService : IPayStubService
    {
        private readonly IAuthService _authService;
        private readonly IPayStubRepository _payStubRepository;
        private readonly IMapper _mapper;

        public PayStubService(IPayStubRepository payStubRepository , IMapper mapper)
        {
            _payStubRepository = payStubRepository;
            _mapper = mapper;
        }

        public async Task<List<PayStubDto>> GetPayStub(GetPayStubRequest getPayStub)
        {
            var PayEntity = await _payStubRepository.GetPayStub(getPayStub.year,getPayStub.month, getPayStub.typePayroll);
            return _mapper.Map<List<PayStubDto>>(PayEntity);
        }
    }
}
