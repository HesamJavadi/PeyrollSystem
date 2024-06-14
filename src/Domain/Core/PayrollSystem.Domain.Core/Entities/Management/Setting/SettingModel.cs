using PayrollSystem.Domain.Core.Entities.Common;
using PayrollSystem.Domain.Core.ValueObjects.Management.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.Management.Setting
{
    public class SettingModel : AggregateRoot<int>
    {
        public LogoPath Logo { get; set; }
        public string ApplicationName { get; set; }
        public string? DashboadDescription { get; set; }
    }
}
