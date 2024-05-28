using Dapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.personnel.PayStatement;
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

        public List<PayStubModel>? GetPayStub(int year, int month)
        {
            var obj = new
            {
                salaryyear = year,
                salarymonth = month, //11
                personelcode = 13
            };
            return _dbConnection.Query<PayStubModel>("SVC_GetMonthlySalaryFicheInfo", obj, commandType: CommandType.StoredProcedure).ToList();
        }

    }
}
