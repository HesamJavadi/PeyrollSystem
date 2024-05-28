using PayrollSystem.Domain.Core.Entities.personnel.PayStatement;
using PayrollSystem.Domain.Core.Entities.personnel.PayStatementDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Data.Personnel.PayStatement
{
    public interface IPayStatementRepository
    {
        List<PayStatementModel> GetPayStatementNumber(int CommitStatementStatus, int StatementStatus, int PersonelStatus, int PersonelCode);
        List<PayStatementDetailsModel> GetPayStatementDetail(int StatementNumber, int type, int PersonelCode);
    }
}
