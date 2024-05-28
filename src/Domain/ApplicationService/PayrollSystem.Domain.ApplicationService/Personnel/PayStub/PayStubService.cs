using AutoMapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Personnel.PayStub
{
    public class PayStubService : IPayStubService
    {
        private readonly IPayStubRepository payStubRepository;
        private readonly IMapper _mapper;

        public PayStubService(IPayStubRepository payStubRepository , IMapper mapper)
        {
            this.payStubRepository = payStubRepository;
            _mapper = mapper;
        }

        public List<PayStubDto> GetPayStub(GetPayStubRequest getPayStub)
        {
            var PayEntity = payStubRepository.GetPayStub(getPayStub.year,getPayStub.month);
            return _mapper.Map<List<PayStubDto>>(PayEntity);
        }
    }
}
