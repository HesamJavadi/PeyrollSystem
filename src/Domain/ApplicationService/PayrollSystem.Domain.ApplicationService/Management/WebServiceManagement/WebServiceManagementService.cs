using AutoMapper;
using PayrollSystem.Domain.ApplicationService.Common;
using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Contracts.Data.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Service.Management.WebServiceManagement;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Management.WebServiceManagement
{
    public class WebServiceManagementService : AppService<WebServiceManagementModel, WebServiceManagementDto, int> , IWebServiceManagementService
    {
        private readonly IWebServiceManagementRepository webServiceManagement;

        public WebServiceManagementService(IWebServiceManagementRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }
    }
}
