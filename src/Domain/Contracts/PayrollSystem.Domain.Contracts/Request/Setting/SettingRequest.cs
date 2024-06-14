using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.Setting
{
    public class SettingRequest
    {
        public IFormFile? Logo { get; set; } 
        public string ApplicationName { get; set; } 
        public string? DashboadDescription { get; set; } 
    }
}
