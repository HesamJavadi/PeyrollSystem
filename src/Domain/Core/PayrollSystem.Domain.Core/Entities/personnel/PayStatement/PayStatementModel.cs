using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.personnel.PayStatement
{
    public class PayStatementModel
    {
        public int StatementNumber { get; set; }
        public string StatementTitle { get; set; }
        public string ExecuteDate { get; set; }

    }
}
