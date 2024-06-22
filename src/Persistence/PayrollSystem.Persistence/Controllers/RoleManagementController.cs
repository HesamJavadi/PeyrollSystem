using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Domain.ApplicationService.Management.Roles;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Request.Auth;
using PayrollSystem.Domain.Contracts.Request.UserRoleAssignment;
using PayrollSystem.Domain.Contracts.Service.Roles;
using PayrollSystem.Domain.Contracts.Service.User;
using PayrollSystem.Domain.Contracts.Utilities;
using System.Security.Claims;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleManagementController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;


        public RoleManagementController(IRoleService roleService , IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetRoles();
            return Ok(roles);
        }

        // GET: api/Roles/{id}
        [HttpGet("{name}")]
        public async Task<IActionResult> GetRoleById(string name)
        {
            var role = await _roleService.GetRole(name);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var response = await _roleService.CreateRole(request);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string roleName)
        {
            var response = await _roleService.UpdateRoleAsync(id, roleName);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var response = await _roleService.DeleteRoleAsync(roleName);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRoleAssignmentRequest model)
        {
            var response = await _userService.AssignRoleToUserAsync(model);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUsers([FromBody] UsersRoleAssignmentRequest model)
        {
            var response = await _userService.AssignRoleToUsersAsync(model);

            if (response.success)
            {
                return Ok(new { Message = response });
            }

            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRoleFromUsers([FromBody] UserRoleAssignmentRequest model)
        {
            var response = await _userService.RemoveRoleFromUserAsync(model);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddClaimToRole([FromBody] AddClaimToRoleRequest model)
        {
            var response = await _roleService.AddClaimToRoleAsync(model);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddClaimToUser([FromBody] AddClaimToUserRequest model)
        {
            var response = await _userService.AddClaimToUserAsync(model);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> SetClaimToUser([FromBody] SetClaimToUserRequest request)
        {
            var response = await _userService.SetClaimToUserAsync(request);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveClaimFromUser(string username, string claimValue)
        {
            var response = await _userService.RemoveClaimFromUserAsync(username, claimValue);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveClaimFromRole(string role, string claimValue)
        {
            var response = await _roleService.RemoveClaimFromRoleAsync(role, claimValue);

            if (response.success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleClaims([FromQuery] string roleName)
        {
            var claims = await _roleService.GetClaimsForRoleAsync(roleName);
            return Ok(claims);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAccess([FromQuery] UserAccessRequest request)
        {
            var access = await _userService.GetUserAccessAsync(request.Username);
            return Ok(access);
        }

        [HttpGet("{role}")]
        public async Task<IActionResult> GetRoleAccess(string role)
        {
            var access = await _userService.GetUserAccessByRoleAsync(role);
            return Ok(access);
        }
    }
}

