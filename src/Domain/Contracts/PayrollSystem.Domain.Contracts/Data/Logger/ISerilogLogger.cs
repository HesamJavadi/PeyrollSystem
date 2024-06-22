using PayrollSystem.Domain.Core.Entities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Data.Logger
{
    public interface ISerilogLogger
    {
        List<SystemLogEntry> GetLogs(int Take, int Skip);
    }
}
