using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Request.WebServiceManagement;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.Management.WebServiceManagement
{
    public interface IWebServiceManagementService : IAppService<WebServiceManagementModel, WebServiceManagementDto, WebServiceManagementRequest, int>
    {
    }
}
