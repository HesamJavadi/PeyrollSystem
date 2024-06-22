using Microsoft.AspNetCore.Identity;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.Request.Auth;
using PayrollSystem.Domain.Contracts.Request.UserRoleAssignment;
using PayrollSystem.Domain.Contracts.Service.Roles;
using PayrollSystem.Domain.Contracts.Utilities;
using PayrollSystem.Domain.Core.Exeptions;
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
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<ServiceResponse> CreateRole(CreateRoleRequest request)
        {
            if (string.IsNullOrEmpty(request.RoleName))
            {
                return ServiceResponse.Fail("Role name cannot be empty");
            }

            var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
            if (roleExists)
            {
                return ServiceResponse.Fail("Role already exists");
            }

            var newRole = new ApplicationRole { Name = request.RoleName };
            var result = await _roleManager.CreateAsync(newRole);
            if (!result.Succeeded)
            {
                return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
            }

            return ServiceResponse.Success("Role created successfully");
        }
        public async Task<List<Claim>> GetClaimsForRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return new List<Claim>();
            }

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            return roleClaims.ToList();
        }
        public async Task<UserAcessDto> GetUserAccess(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {

                var _dictionary = new Dictionary<string, List<string>>();
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    var ClaimsRole = await GetClaimsForRoleAsync(role);
                    _dictionary.Add(role, ClaimsRole.Select(x => x.Value).ToList());
                }
                var userClaims = await _userManager.GetClaimsAsync(user);

                UserAcessDto userAcessDto = new UserAcessDto
                {
                    id = Guid.NewGuid(),
                    avatar = user.Avatar,
                    username = user.UserName,
                    nationalCode = user.nationalCode,
                    isActive = user.isActive,
                    userCode = user.pepCode,
                    phone = user.PhoneNumber,
                    lastActive = user.LastActive.ToString("yyyy/MM/dd"),
                    Roles = _dictionary,
                    Claims = userClaims.ToList(),
                };

                return userAcessDto;

            }

            throw new Exception("UserNotFound");
        }

        public async Task<List<UserAcessDto>> GetUserAccessByRole(string role)
        {
            var adminRole = await _roleManager.FindByNameAsync(role);
            List<UserAcessDto> userAccessList = new List<UserAcessDto>();
            if (adminRole != null)
            {
                var users = await _userManager.GetUsersInRoleAsync(adminRole.Name);
                var _dictionary = new Dictionary<string, List<string>>();

                    var ClaimsRole = await GetClaimsForRoleAsync(role);
                    _dictionary.Add(role, ClaimsRole.Select(x => x.Value).ToList());

                foreach (var user in users)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    UserAcessDto userAcessDto = new UserAcessDto
                    {
                        id = Guid.NewGuid(),
                        avatar = user.Avatar,
                        username = user.UserName,
                        nationalCode = user.nationalCode,
                        isActive = user.isActive,
                        userCode = user.pepCode,
                        phone = user.PhoneNumber,
                        lastActive = user.LastActive.ToString("yyyy/MM/dd"),
                        Roles = _dictionary,
                        Claims = userClaims.ToList(),
                    };
                    userAccessList.Add(userAcessDto);
                }
                return userAccessList;

            }

            throw new Exception("UserNotFound");
        }

        public async Task<ServiceResponse> RemoveRoleFromUser(UserRoleAssignmentRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return ServiceResponse.Fail(new List<string> { " کاربر مورد نظر پیدا نشد" });
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
            {
                return ServiceResponse.Fail(new List<string> { " نقش مورد نظر پیدا نشد " });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

            if (result.Succeeded)
            {
                return ServiceResponse.Success("دسترسی کاربر گرفته شد");
            }

            return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
        }

        public async Task<ServiceResponse> UpdateRole(string id, string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return ServiceResponse.Fail("نام نقش نمیتواند خالی باشد");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return ServiceResponse.Fail("نقش مورد نظر پیدا نشد");
            }

            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
            }

            return ServiceResponse.Success("نقش مورد نظر با موفقیت اپدیت شد");
        }

        public async Task<ServiceResponse> UpdateRoleAsync(string id, string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return ServiceResponse.Fail("Role name cannot be empty");
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                throw new NotFoundException("Role with ID '{0}' not found", id);
            }

            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
            }

            return ServiceResponse.Success("Role updated successfully");
        }

        public async Task<ServiceResponse> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return ServiceResponse.Fail("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
            }

            return ServiceResponse.Success("Role deleted successfully");
        }

        public async Task<ServiceResponse> AddClaimToRoleAsync(AddClaimToRoleRequest model)
        {
            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
            {
                return ServiceResponse.Fail("Role not found");
            }

            var role = await _roleManager.FindByNameAsync(model.RoleName);
            var claim = new Claim($"Permission{model.RoleName}", model.ClaimValue);
            var result = await _roleManager.AddClaimAsync(role, claim);

            if (result.Succeeded)
            {
                return ServiceResponse.Success();
            }

            return ServiceResponse.Fail("Failed to add claim to role");
        }

        public async Task<ServiceResponse> RemoveClaimFromRoleAsync(string role, string claimValue)
        {
            var roleName = await _roleManager.FindByNameAsync(role);
            if (roleName == null)
            {
                return ServiceResponse.Fail("Role not found");
            }

            var claim = new Claim($"Permission{role}", claimValue);
            var result = await _roleManager.RemoveClaimAsync(roleName, claim);

            if (result.Succeeded)
            {
                return ServiceResponse.Success("Claim removed successfully");
            }

            return ServiceResponse.Fail("Failed to remove claim from role");
        }

        public List<ApplicationRole>? GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }
        public async Task<ApplicationRole?> GetRole(string role)
        {
            var _res = await _roleManager.FindByNameAsync(role);
            return _res;
        }
    }

}
