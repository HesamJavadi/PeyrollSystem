using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.Identity.User
{
    public class UserModel : IdentityUser
    {
        public int pepCode { get; set; }
    }
}
