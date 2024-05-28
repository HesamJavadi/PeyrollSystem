using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.Personnel.PayStub
{
    public interface IPayStubService
    {
        List<PayStubDto> GetPayStub(GetPayStubRequest getPay);
    }
}
