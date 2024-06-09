using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.Auth
{
    public class ForgotPasswordRequest
    {
        public string username { get; set; }
    }
}
