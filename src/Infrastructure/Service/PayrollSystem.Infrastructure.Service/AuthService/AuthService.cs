using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Domain.Contracts.Request.Users;
using PayrollSystem.Domain.Contracts.Utilities;
using PayrollSystem.Infrastructure.Service.UploadFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PayrollSystem.Infrastructure.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUploadFileHandlerService _uploadFileHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IUploadFileHandlerService uploadFileHandler)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _uploadFileHandler = uploadFileHandler;
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

        public async Task<ResponseGeneric<UserDto>> UpdateUser(string username, UserUpdateRequest request)
        {
            var user = await _userManager.FindByNameAsync(username);
           
            if (user != null)
            {
                try
                {
                    if (request.avatar != null)
                    {
                        user.Avatar = _uploadFileHandler.ReturnFilePath($"avatar\\{username}", request.avatar.FileName);
                    }
                    user.UserName = request.username;
                    user.pepCode = request.userCode;
                    user.nationalCode = request.nationalCode;
                    user.PhoneNumber = request.phone;
                    if (request.isActive.HasValue)
                    {
                        user.isActive = request.isActive.Value;
                    }
                    var Updateuser = await _userManager.UpdateAsync(user);
                    if (Updateuser.Succeeded)
                    {
 
                        if (request.avatar != null)
                        {
                            await _uploadFileHandler.SaveFileAsync(request.avatar, $"avatar\\{username}");
                        }
                        var userDto = new UserDto
                        {
                            id = Guid.NewGuid(),
                            avatar = user.Avatar,
                            username = user.UserName,
                            nationalCode = user.nationalCode,
                            isActive = user.isActive,
                            userCode = user.pepCode,
                            phone = user.PhoneNumber,
                            lastActive = user.LastActive.ToString("yyyy/MM/dd")
                        };
                        return ResponseGeneric<UserDto>.Success(userDto);
                    }

                    throw new Exception(Updateuser.Errors.First().Description);
                }
                catch (Exception error)
                {
                    throw new Exception(error.Message);
                }
            }
            throw new Exception("not found user");
        }
    }
}
