﻿using AutoMapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Request.PayStatement;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Service.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Personnel.PayStatement
{
    public class PayStatementService : IPayStatementService
    {
        private readonly IPayStatementRepository payStubRepository;
        private readonly IMapper _mapper;

        public PayStatementService(IPayStatementRepository payStubRepository, IMapper mapper)
        {
            this.payStubRepository = payStubRepository;
            _mapper = mapper;
        }

        public List<PayStatementDto> GetPayStatementNumber(GetPayStatementRequest getPay)
        {
            var PayEntity = payStubRepository.GetPayStatementNumber(0,0,0,13);
            return _mapper.Map<List<PayStatementDto>>(PayEntity);
        }

        public List<PayStatementDetailDto> GetPayStatementDetail(GetPayStatementRequest getPay)
        {
            var PayEntity = payStubRepository.GetPayStatementDetail(getPay.StatementNumber, getPay.Type, 13);
            return _mapper.Map<List<PayStatementDetailDto>>(PayEntity);
        }
    }
}