using PayrollSystem.Domain.Contracts.Dtos.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.Management.Setting
{
    public class SettingDto : Dto<int>
    {
        public string Logo { get; set; }
        public string ApplicationName { get; set; }
        public string? DashboadDescription { get; set; }
    }
}
