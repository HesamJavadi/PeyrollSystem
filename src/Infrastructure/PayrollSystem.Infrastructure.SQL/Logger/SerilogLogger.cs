using PayrollSystem.Domain.Contracts.Data.Logger;
using PayrollSystem.Domain.Core.Entities.Logger;
using PayrollSystem.Infrastructure.SQL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PayrollSystem.Infrastructure.SQL.Logger
{

    public class SerilogLogger : ISerilogLogger
    {
        ApplicationDbContext _context;

        public SerilogLogger(ApplicationDbContext context)
        {
            _context = context;   
        }

        public List<SystemLogEntry> GetLogs(int Take,int Skip)
        {
           return _context.Set<SystemLogEntry>().OrderByDescending(x=>x.Id).Skip(Skip).Take(Take).ToList();
        }
    }
}
