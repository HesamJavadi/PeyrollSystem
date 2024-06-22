using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.Request.Auth;
using PayrollSystem.Domain.Contracts.Request.UserRoleAssignment;
using PayrollSystem.Domain.Contracts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.User
{
    public interface IUserService
    {
        Task<ServiceResponse> AssignRoleToUserAsync(UserRoleAssignmentRequest model);
        Task<ServiceResponse> AssignRoleToUsersAsync(UsersRoleAssignmentRequest model);
        Task<ServiceResponse> RemoveRoleFromUserAsync(UserRoleAssignmentRequest model);
        Task<ServiceResponse> AddClaimToUserAsync(AddClaimToUserRequest model);
        Task<ServiceResponse> SetClaimToUserAsync(SetClaimToUserRequest request);
        Task<ServiceResponse> RemoveClaimFromUserAsync(string username, string claimValue);
        Task<List<string>> GetUserAccessAsync(string username);
        Task<List<UserDto>> GetUserAccessByRoleAsync(string role);

        Task<ServiceResponse> UpdateUserFromDB();
    }
}
