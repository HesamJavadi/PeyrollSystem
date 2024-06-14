using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Domain.Contracts.Request.Common;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using PayrollSystem.Domain.Contracts.Request.Users;
using PayrollSystem.Domain.Contracts.Utilities;
using PayrollSystem.Infrastructure.Service.AuthService;
using System.Data.Entity;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class PersonnelManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;


        public PersonnelManagementController(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] PageInfoRequest pageInfo)
        {
            try
            {
                var query = _userManager.Users
                    .Where(x => x.UserName.Contains(pageInfo.query) || pageInfo.query == null)
                    .OrderBy(x => x.UserName);

                var totalUsers = query.Count();

                var users = query
                    .Skip((pageInfo.pageIndex - 1) * pageInfo.pageSize)
                    .Take(pageInfo.pageSize)
                    .Select(user => new UserDto
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

                var response = new PaginatedResponse<UserDto>
                {
                    TotalCount = totalUsers,
                    Items = users
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> ActiveUser([FromBody] string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                try
                {
                    user.isActive = !user.isActive;
                    var users = await _userManager.UpdateAsync(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new { username, user.isActive });
            }
            return BadRequest("User Is Not Defind");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUsers([FromQuery] string username,[FromForm] UserUpdateRequest value)
        {
            var response = await _authService.UpdateUser(username, value);
            if (response.success)
            {
                return Ok(response.data);
            }
            return BadRequest(response.errors);

        }


        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound($"User with ID {username} not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(500, "An error occurred while deleting the user.");
            }

            return Ok($"User with ID {username} has been deleted.");
        }
    }
}
