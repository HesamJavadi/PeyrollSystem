using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Contracts.Request.PayStub;
using System.Data.Entity;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PersonnelManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonnelManagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = users.Select(user => new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();
            return Ok(userDtos);
        }
    }
}
