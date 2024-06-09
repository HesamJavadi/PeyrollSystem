using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.Request.Common;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using System.Data.Entity;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class PersonnelManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonnelManagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] PageInfoRequest pageInfo)
        {
            try
            {
                var users = _userManager.Users.OrderBy(x => x.UserName).Skip((pageInfo.pageIndex - 1) * pageInfo.pageSize).Take(pageInfo.pageSize)
                    .Where(x => x.UserName.Contains(pageInfo.query) || pageInfo.query == null);
                var userDtos = users.Select(user => new UserDto
                {
                    id = Guid.NewGuid(),
                    username = user.UserName,
                    nationalCode = user.nationalCode,
                    isActive = user.isActive,
                    userCode = user.pepCode,
                    phone = user.PhoneNumber,
                    lastActive = user.LastActive.ToString("yyyy/MM/dd")
                }).ToList();
                return Ok(userDtos);
            }catch(Exception e)
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
        public async Task<IActionResult> UpdateUsers([FromQuery] string username,[FromBody] UserDtoRequest value)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                try
                {
                    user.UserName = value.username;
                    user.pepCode = value.userCode;
                    user.nationalCode = value.nationalCode;
                    user.PhoneNumber = value.phone;
                    if (value.isActive.HasValue)
                    {
                        user.isActive = value.isActive.Value;
                    }
                    var users = await _userManager.UpdateAsync(user);
                    if (users.Succeeded)
                    {
                        return Ok(value);
                    }

                    return BadRequest(users.Errors);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("کاربر یافت نشد");
        }
    }
}
