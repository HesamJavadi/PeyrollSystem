﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.Logger
{
    public class SerilogLoggerDto
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
    }
}
