using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.PayStub
{
    public class GetPayStubRequest
    {
        public int year { get; set; }
        public int month { get; set; }
    }
}
