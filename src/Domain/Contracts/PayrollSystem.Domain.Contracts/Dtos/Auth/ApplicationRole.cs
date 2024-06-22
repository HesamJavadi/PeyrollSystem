using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace PayrollSystem.Domain.Contracts.Dtos.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public string? FA_name { get; set; }
    }
}
