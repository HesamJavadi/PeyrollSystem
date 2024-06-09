using Dapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Domain.Core.Entities.personnel.PayStatement;
using PayrollSystem.Domain.Core.Entities.personnel.PayStatementDetails;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.Dapper.PayStatement
{
    public class PayStatementRepository : IPayStatementRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IAuthService _authService;

        public PayStatementRepository(IDbConnection dbConnection, IAuthService authService)
        {
            _dbConnection = dbConnection;
            _authService = authService;
        }

        public async Task<List<PayStatementModel>?> GetPayStatementNumber(int CommitStatementStatus, int StatementStatus, int PersonelStatus)
        {
            var user = await _authService.GetCurrentUSerAsync();
            var obj = new
            {
                CommitStatementStatus = 0,
                StatementStatus = 0,
                PersonelStatus = 0, //11
                PersonelCode = user.pepCode,
            };
            return _dbConnection.Query<PayStatementModel>("SVC_GetPersonelStatementNumber", obj, commandType: CommandType.StoredProcedure).ToList();
        }

        public async Task<List<PayStatementDetailsModel>?> GetPayStatementDetail(int StatementNumber,int type)
        {
            var user = await _authService.GetCurrentUSerAsync();
            var obj = new
            {
                CommitStatementStatus = 0,
                StatementStatus = 0,
                PersonelStatus = 0, //11
                PersonelCode = user.pepCode,
                StatementNumber = StatementNumber,
                Type = type,
            };
            return _dbConnection.Query<PayStatementDetailsModel>("SVC_GetPersonelStatement", obj, commandType: CommandType.StoredProcedure).ToList();
        }

    }
}
