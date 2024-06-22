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
using System.Data.Entity;
using PayrollSystem.Domain.Contracts.Request.Auth;
using PayrollSystem.Domain.Contracts.Service.Roles;
using PayrollSystem.Domain.Contracts.Utilities;

namespace PayrollSystem.Persistence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        private readonly ISendSms _sendSms;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ISendSms sendSms,
            IRoleService roleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _sendSms = sendSms;
            _roleService = roleService;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Signin([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (user.isActive)
                {
                    var userRoles = await _userManager.GetRolesAsync(user); // Get roles for the user

                    var claims = new List<Claim>
            {  
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

                    var userClaim = await _userManager.GetClaimsAsync(user);
                    claims.AddRange(userClaim);
                    // Add roles as claims
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));

                        // Retrieve claims for the role
                        var roleClaims = await _roleService.GetClaimsForRoleAsync(role);
                        claims.AddRange(roleClaims);
                    }

                    var expireDate = DateTime.UtcNow.AddDays(30);
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: expireDate,
                        signingCredentials: creds);

                    var authority = new List<string>();
                    authority.Add("admin");

                    userRoles.Add("none");
                    var res = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        date = expireDate,
                        user = new
                        {
                            userName = user.UserName,
                            email = "",
                            authority = userRoles, 
                            //authority = authority, 
                            avatar = ""
                        }
                    };
                    return Ok(res);
                }
            }

            return Unauthorized(ServiceResponse.Fail("نام کاربری و یا رمز عبور اشتباه است"));
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
            var user = _userManager.Users.FirstOrDefault(x => x.nationalCode == request.NationalCode);
            if (user != null)
            {
                Random random = new Random();
                var verificationCode = random.Next(100000, 999999).ToString();
                string smsText = $"کد :  {verificationCode}";

                user.PasswordResetCode = verificationCode;
                user.PasswordResetCodeExpiration = DateTime.UtcNow.AddMinutes(15);
                await _userManager.UpdateAsync(user);

                _sendSms.send(smsText, new string[] { user.PhoneNumber });
                return Ok(new { message = "Verification code sent to your phone number." , code = smsText });
            }
            return BadRequest(ServiceResponse.Fail("کاربر پیدا نشد"));
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.nationalCode == request.NationalCode);
            if (user != null)
            {
                if (user.PasswordResetCode == request.verifyCode && user.PasswordResetCodeExpiration > DateTime.UtcNow)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    return Ok(new { token , request.NationalCode });
                }
                return BadRequest(ServiceResponse.Fail("کد وارد شده اشتباه است"));
            }
            return BadRequest(ServiceResponse.Fail("کاربر پیدا نشد"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] PayrollSystem.Domain.Contracts.Request.Auth.ResetPasswordRequest request)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.nationalCode == request.NationalCode);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
                if (result.Succeeded)
                {
                    user.PasswordResetCode = null;
                    user.PasswordResetCodeExpiration = null;
                    await _userManager.UpdateAsync(user);

                    return Ok(ServiceResponse.Success("با موفقیت ویرایش شد"));
                }
                return BadRequest(ServiceResponse.Fail("ویرایش رمز عبور با شکست مواجه شد"));
            }
            return BadRequest(ServiceResponse.Fail("کاربر پیدا نشد"));
        }
    }
}
