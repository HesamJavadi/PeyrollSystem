﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace PayrollSystem.Domain.Contracts.Dtos.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string pepCode { get; set; }
    }
}
