using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.Auth
{
    public class ResetPasswordRequest
    {
        public string NationalCode { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
