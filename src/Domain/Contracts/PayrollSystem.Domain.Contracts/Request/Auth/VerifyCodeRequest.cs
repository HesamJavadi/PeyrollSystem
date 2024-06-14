using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.Auth
{
    public class VerifyCodeRequest
    {
        public string NationalCode { get; set; }
        public string verifyCode { get; set; }
    }
}
