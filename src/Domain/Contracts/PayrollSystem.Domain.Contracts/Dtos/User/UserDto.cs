using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.User
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string avatar { get; set; }
        public string username { get; set; }
        public string lastActive { get; set; }
        public bool isActive { get; set; }
        public string? nationalCode { get; set; }
        public string? userCode { get; set; }
        public string? phone { get; set; }
    }
}
