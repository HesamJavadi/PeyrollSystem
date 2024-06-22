using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PayrollSystem.Domain.Contracts.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.Auth
{
    public class UserAcessDto : UserDto
    {
        public Dictionary<string,List<string>> Roles { get; set; }
        public List<Claim> Claims { get; set; }
    }

}
