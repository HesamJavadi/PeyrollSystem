using Microsoft.AspNetCore.Identity;
using PayrollSystem.Domain.Contracts.Data.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.Request.Auth;
using PayrollSystem.Domain.Contracts.Request.UserRoleAssignment;
using PayrollSystem.Domain.Contracts.Service.User;
using PayrollSystem.Domain.Contracts.Utilities;
using PayrollSystem.Domain.Core.Exeptions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUserRepository _userWindowsRepository;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUserRepository userWindowsRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userWindowsRepository = userWindowsRepository;
    }

    public async Task<ServiceResponse> AssignRoleToUserAsync(UserRoleAssignmentRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return ServiceResponse.Fail("User not found");
        }

        var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
        if (!roleExists)
        {
            return ServiceResponse.Fail("Role not found");
        }

        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
        if (result.Succeeded)
        {
            return ServiceResponse.Success("با موفقیت دسترسی ها داده شد");
        }

        return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<ServiceResponse> AssignRoleToUsersAsync(UsersRoleAssignmentRequest model)
    {
        foreach (var username in model.Usernames)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return ServiceResponse.Fail("User not found");
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
            {
                return ServiceResponse.Fail("Role not found");
            }

            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (!result.Succeeded)
            {
                return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
            }
        }

        return ServiceResponse.Success("Roles assigned successfully");
    }

    public async Task<ServiceResponse> RemoveRoleFromUserAsync(UserRoleAssignmentRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return ServiceResponse.Fail("User not found");
        }

        var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
        if (!roleExists)
        {
            return ServiceResponse.Fail("Role not found");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
        if (result.Succeeded)
        {
            return ServiceResponse.Success("Role removed successfully");
        }

        return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<ServiceResponse> AddClaimToUserAsync(AddClaimToUserRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return ServiceResponse.Fail("User not found");
        }

        var claim = new Claim($"Permission{model.Username}", model.ClaimValue);
        var userClaims = await _userManager.GetClaimsAsync(user);
        if (userClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
        {
            return ServiceResponse.Fail("Claim already exists");
        }

        var result = await _userManager.AddClaimAsync(user, claim);
        if (result.Succeeded)
        {
            return ServiceResponse.Success("Claim added successfully");
        }

        return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<ServiceResponse> SetClaimToUserAsync(SetClaimToUserRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            return ServiceResponse.Fail("User not found");
        }

        List<string> options = new List<string> { "Create", "Update", "Delete", "Read" };
        var mustCreate = request.ClaimsValue.Where(x => options.Contains(x)).ToList();
        var mustDelete = options.Where(x => !request.ClaimsValue.Contains(x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        foreach (var claimValue in mustCreate)
        {
            var claim = new Claim($"Permission{request.RoleName}{request.Username}", claimValue);
            if (!userClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
            {
                var addResult = await _userManager.AddClaimAsync(user, claim);
                if (!addResult.Succeeded)
                {
                    return ServiceResponse.Fail(addResult.Errors.Select(e => e.Description).ToList());
                }
            }
        }

        foreach (var claimValue in mustDelete)
        {
            var claim = new Claim($"Permission{request.RoleName}{request.Username}", claimValue);
            var removeResult = await _userManager.RemoveClaimAsync(user, claim);
            if (!removeResult.Succeeded)
            {
                return ServiceResponse.Fail(removeResult.Errors.Select(e => e.Description).ToList());
            }
        }

        return ServiceResponse.Success("Claims updated successfully");
    }

    public async Task<ServiceResponse> RemoveClaimFromUserAsync(string username, string claimValue)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return ServiceResponse.Fail("User not found");
        }

        var claim = new Claim($"Permission{username}", claimValue);
        var result = await _userManager.RemoveClaimAsync(user, claim);
        if (result.Succeeded)
        {
            return ServiceResponse.Success("Claim removed successfully");
        }

        return ServiceResponse.Fail(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<List<string>> GetUserAccessAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var claims = await _userManager.GetClaimsAsync(user);
        return claims.Select(c => c.Value).ToList();
    }

    public async Task<List<UserDto>> GetUserAccessByRoleAsync(string role)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(role);

        var users = usersInRole.Select(user => new UserDto
        {
            id = Guid.NewGuid(),
            avatar = user.Avatar,
            username = user.UserName,
            nationalCode = user.nationalCode,
            isActive = user.isActive,
            userCode = user.pepCode,
            phone = user.PhoneNumber,
            lastActive = user.LastActive.ToString("yyyy/MM/dd")
        }).ToList();
        return users;
    }


    public async Task<ServiceResponse> UpdateUserFromDB()
    {
        var windowsUser = await _userWindowsRepository.GetUsers();
        var result = new IdentityResult();
        foreach (var usr in windowsUser)
        {
            var user = new ApplicationUser
            {
                UserName = usr.NationalCode,
                pepCode = usr.PersonelCode,
                nationalCode = usr.NationalCode,
                PhoneNumber = usr.Phone,
                isActive = usr.IsActive.HasValue ? usr.IsActive.Value : false
            };

            result = await _userManager.CreateAsync(user, "@@1234HJKLhjklsdc&&iehfwi@@#ccmnocdcdc");
        }

        if (result.Succeeded)
        {
            return ServiceResponse.Success("کاربران اپدیت شدند");
        }
        return ServiceResponse.Fail(result.Errors.Select(x=>x.Description).ToList());

    }
}
