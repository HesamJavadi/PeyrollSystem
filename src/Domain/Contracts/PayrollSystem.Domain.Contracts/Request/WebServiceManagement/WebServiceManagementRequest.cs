using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Request.WebServiceManagement
{
    public class WebServiceManagementRequest
    {
        public string? Name { get; set; }
        public string? URL { get; set; }
    }
}
