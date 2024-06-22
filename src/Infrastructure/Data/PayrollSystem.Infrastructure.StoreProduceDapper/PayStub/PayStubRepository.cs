using Dapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.InfraService;
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
        private readonly IAuthService _authService;
        public PayStubRepository(IDbConnection dbConnection, IAuthService authService)
        {
            _dbConnection = dbConnection;
            _authService = authService;
        }

        public async Task<List<PayStubModel>?> GetPayStub(int year, int month, int type)
        {
            var user = await _authService.GetCurrentUSerAsync();
            var obj = new
            {
                salaryyear = year,
                salarymonth = month, //11
                personelcode = user.pepCode
            };
            if (type == 1)
            {
                return _dbConnection.Query<PayStubModel>("SVC_GetMonthlySalaryFicheInfo", obj, commandType: CommandType.StoredProcedure).ToList();

            }
            else if(type == 2)
            {
                return _dbConnection.Query<PayStubModel>("SVC_GetSalarySecondPaymentFicheInfo2 ", obj, commandType: CommandType.StoredProcedure).ToList();

            }

            return _dbConnection.Query<PayStubModel>("SVC_GetDiffMonthlySalaryFicheInfo2", obj, commandType: CommandType.StoredProcedure).ToList();


        }

    }
}
