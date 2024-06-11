using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PayrollSystem.Domain.Contracts.InfraService;
using Microsoft.AspNetCore.Identity.Data;
using Azure.Core;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ISendSms _sendSms;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ISendSms sendSms)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _sendSms = sendSms;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Singin([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (user.isActive)
                {
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };
                    var expireDate = DateTime.UtcNow.AddMinutes(30);
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    // -----------------------------------------------------------------------------
                    PersianCalendar persianCalendar = new PersianCalendar();
                    var date = DateTime.Now;
                    int year = persianCalendar.GetYear(date);
                    int month = persianCalendar.GetMonth(date);
                    int day = persianCalendar.GetDayOfMonth(date);
                    user.LastActive = DateTime.Parse($"{year}/{month:D2}/{day:D2}");
                    var users = await _userManager.UpdateAsync(user);
                    //------------------------------------------------------------------------------
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: expireDate,
                        signingCredentials: creds);

                    var authority = new List<string>();
                    authority.Add("admin");
                    var res = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        date = expireDate,
                        user = new
                        {
                            userName = user.UserName,
                            email = "",
                            authority = authority,
                            avatar = ""
                        }
                    };
                    return Ok(res);
                }
            }

            return Unauthorized();
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                pepCode = "-"
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("signout")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            
            PersianCalendar persianCalendar = new PersianCalendar();
            var date = DateTime.Now;
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);
            user.LastActive = DateTime.Parse($"{year}/{month:D2}/{day:D2}");
            await _userManager.UpdateAsync(user);
            return Ok();
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] PayrollSystem.Domain.Contracts.Request.Auth.ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.username);
            if (user != null)
            {
                Random Random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
                var newPassword = new string(Enumerable.Repeat(chars, 16)
                  .Select(s => s[Random.Next(s.Length)]).ToArray());
                string smsText = $"پسورد جدید شما : {newPassword}";
                if (user.PhoneNumber != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                    if (result.Succeeded)
                    {
                        _sendSms.send(smsText, new string[] { user.PhoneNumber });
                        return Ok(request);
                    }
                }
                return BadRequest("شماره موبایل ست نشده است");

            }
            return BadRequest("کاربر پیدا نشد");
        }
    }
}
