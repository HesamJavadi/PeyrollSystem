using PayrollSystem.Domain.Contracts.Dtos.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement
{
    public class WebServiceManagementDto : Dto<int>
    {
        public string Name { get; set; }     
        public string URL { get; set; }     
    }
}
