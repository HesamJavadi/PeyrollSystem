using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.UserRoleAssignment
{
    public class UserRoleAssignmentRequest
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
