using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.Users
{
    public class UserPersonnelModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonelCode { get; set; }
        public string? NationalCode { get; set; }
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }
    }
}
