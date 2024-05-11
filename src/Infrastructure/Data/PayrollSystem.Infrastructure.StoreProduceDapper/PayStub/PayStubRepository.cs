using Dapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.Dapper.PayStub
{
    public class PayStubRepository : IPayStubRepository
    {
        private readonly IDbConnection _dbConnection;

        public PayStubRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public PayStubModel? GetPayStub()
        {
            return _dbConnection.Query<PayStubModel>("select 1 as id").FirstOrDefault(); ;
        }

    }
}
