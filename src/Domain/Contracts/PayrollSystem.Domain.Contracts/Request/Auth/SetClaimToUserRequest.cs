using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.Auth
{
    public class SetClaimToUserRequest
    {
        public string Username { get; set; } 
        public string RoleName { get; set; } 
        public string[] ClaimsValue { get; set; } 
    }
}
