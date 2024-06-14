using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Domain.Contracts.Dtos.User;
using PayrollSystem.Domain.Contracts.Request.Users;
using PayrollSystem.Domain.Contracts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.InfraService
{
    public interface IAuthService
    {
        Task<ApplicationUser> GetCurrentUSerAsync();
        Task<ResponseGeneric<UserDto>> UpdateUser(string username, UserUpdateRequest request);
    }
}
