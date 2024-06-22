using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Request.Auth;
using PayrollSystem.Domain.Contracts.Request.UserRoleAssignment;
using PayrollSystem.Domain.Contracts.Utilities;
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
        Task<List<Claim>> GetClaimsForRoleAsync(string roleName);
        Task<UserAcessDto> GetUserAccess(string username);
        Task<List<UserAcessDto>> GetUserAccessByRole(string role);
        List<ApplicationRole>? GetRoles();
        Task<ApplicationRole?> GetRole(string role);


        Task<ServiceResponse> CreateRole(CreateRoleRequest request);
        Task<ServiceResponse> RemoveRoleFromUser(UserRoleAssignmentRequest model);
        Task<ServiceResponse> UpdateRoleAsync(string id, string roleName);

        Task<ServiceResponse> DeleteRoleAsync(string roleName);
        Task<ServiceResponse> AddClaimToRoleAsync(AddClaimToRoleRequest model);
        Task<ServiceResponse> RemoveClaimFromRoleAsync(string role, string claimValue);


    }
}
