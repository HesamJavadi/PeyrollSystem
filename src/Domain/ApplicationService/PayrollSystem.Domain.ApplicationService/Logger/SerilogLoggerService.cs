using AutoMapper;
using PayrollSystem.Domain.Contracts.Data.Logger;
using PayrollSystem.Domain.Contracts.Dtos.Logger;
using PayrollSystem.Domain.Contracts.Service.Logger;
using PayrollSystem.Domain.Core.Entities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Logger
{
    public class SerilogLoggerService : ISerilogLoggerService
    {
        private readonly ISerilogLogger _serilogLogger;
        private readonly IMapper _mapper;

        public SerilogLoggerService(ISerilogLogger serilogLogger, IMapper mapper)
        {
            _serilogLogger = serilogLogger;
            _mapper = mapper;
        }
        public List<SerilogLoggerDto> GetLogs(int Take, int Skip)
        {
           var entity = _serilogLogger.GetLogs(Take,Skip);
            return _mapper.Map<List<SerilogLoggerDto>>(entity);
        }
    }
}
