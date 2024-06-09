using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.InfraService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUSerAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return null;
            }
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
