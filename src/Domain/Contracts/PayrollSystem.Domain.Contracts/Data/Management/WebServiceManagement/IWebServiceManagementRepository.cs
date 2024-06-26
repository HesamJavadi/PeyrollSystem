﻿using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Data.Management.WebServiceManagement
{
    public interface IWebServiceManagementRepository : IBaseRepository<WebServiceManagementModel,int>
    {
    }
}
