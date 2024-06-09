using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.User
{
    public class UserDtoRequest
    {
        public string username { get; set; }
        public string? nationalCode { get; set; }
        public string? userCode { get; set; }
        public string? phone { get; set; }
        public bool? isActive { get; set; }
    }
}
