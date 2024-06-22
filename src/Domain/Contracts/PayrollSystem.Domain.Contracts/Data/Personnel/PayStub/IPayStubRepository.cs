using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Data.Personnel.PayStub
{
    public interface IPayStubRepository
    {
        Task<List<PayStubModel>> GetPayStub(int year,int month,int type);
    }
}
