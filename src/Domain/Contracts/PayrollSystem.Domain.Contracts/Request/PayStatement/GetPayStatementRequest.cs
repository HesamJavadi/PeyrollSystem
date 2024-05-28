using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.PayStatement
{
    public class GetPayStatementRequest
    {
        public int StatementNumber { get; set; }
        public int Type { get; set; }
    }
}
