using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Contracts.Dtos.Management.Setting;
using PayrollSystem.Domain.Contracts.Request.Setting;
using PayrollSystem.Domain.Core.Entities.Management.Setting;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.Management.Setting
{
    public interface ISettingService : IAppService<SettingModel, SettingDto, SettingRequest,  int>
    {
        /// <summary>
        /// return first or default of setting table
        /// </summary>
        /// <returns></returns>
        SettingDto GetDefaultSetting();
    }
}
