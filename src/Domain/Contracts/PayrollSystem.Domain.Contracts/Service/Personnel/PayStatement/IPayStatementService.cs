using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStatementDetail;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Request.PayStatement;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.Personnel.PayStatement
{
    public interface IPayStatementService
    {
        Task<List<PayStatementDto>> GetPayStatementNumber(GetPayStatementRequest getPayStatement);
        Task<List<PayStatementDetailDto>> GetPayStatementDetail(GetPayStatementRequest getPay);
    }
}
