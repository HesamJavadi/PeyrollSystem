using Dapper;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
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

        public PayStatementRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<PayStatementModel>? GetPayStatementNumber(int CommitStatementStatus, int StatementStatus, int PersonelStatus, int PersonelCode)
        {
            var obj = new
            {
                CommitStatementStatus = 0,
                StatementStatus = 0,
                PersonelStatus = 0, //11
                PersonelCode = 1
            };
            return _dbConnection.Query<PayStatementModel>("SVC_GetPersonelStatementNumber", obj, commandType: CommandType.StoredProcedure).ToList();
        }

        public List<PayStatementDetailsModel>? GetPayStatementDetail(int StatementNumber,int type,int PersonelCode)
        {
            var obj = new
            {
                CommitStatementStatus = 0,
                StatementStatus = 0,
                PersonelStatus = 0, //11
                PersonelCode = PersonelCode,
                StatementNumber = StatementNumber,
                Type = type,
            };
            return _dbConnection.Query<PayStatementDetailsModel>("SVC_GetPersonelStatement", obj, commandType: CommandType.StoredProcedure).ToList();
        }

    }
}
