﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.Bases
{
    public class Dto<TId>
    {
        public TId id { get; set; }
    }
}
