using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Data.Personnel.PayStub
{
    public interface IUserRepository
    {
        Task<List<UserPersonnelModel>> GetUsers();
    }
}
