using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.Roles
{
    public interface IRoleService
    {
        Task AddClaimToRole(string roleName, string claimType, string claimValue);
        Task<IList<Claim>> GetRoleClaims(string roleName);
    }
}
