using PayrollSystem.Domain.Core.Entities.Common;
using PayrollSystem.Domain.Core.ValueObjects.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement
{
    public class WebServiceManagementModel : AggregateRoot<int>
    {
        public string Name { get; set; }
        public WebServiceURL URL { get; set; }
    }
}
