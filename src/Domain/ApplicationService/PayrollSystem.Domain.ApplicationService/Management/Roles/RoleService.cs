using Microsoft.AspNetCore.Identity;
using PayrollSystem.Domain.Contracts.Service.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Management.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AddClaimToRole(string roleName, string claimType, string claimValue)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var claim = new Claim(claimType, claimValue);
                await _roleManager.AddClaimAsync(role, claim);
            }
        }

        public async Task<IList<Claim>> GetRoleClaims(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role != null ? await _roleManager.GetClaimsAsync(role) : new List<Claim>();
        }
    }

}
