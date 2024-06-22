using PayrollSystem.Domain.Contracts.Dtos.Logger;
using PayrollSystem.Domain.Core.Entities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Service.Logger
{
    public interface ISerilogLoggerService
    {
        List<SerilogLoggerDto> GetLogs(int Take, int Skip);
    }
}
