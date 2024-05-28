using AutoMapper;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Contracts.Dtos.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStatement;
using PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub;
using PayrollSystem.Domain.Core.Entities.Common;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using PayrollSystem.Domain.Core.Entities.personnel.PayStatement;
using PayrollSystem.Domain.Core.Entities.personnel.PayStatementDetails;
using PayrollSystem.Domain.Core.Entities.personnel.PayStub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<WebServiceManagementModel, WebServiceManagementDto>().ReverseMap();
            CreateMap<PayStubModel, PayStubDto>().ReverseMap();
            CreateMap<PayStatementModel, PayStatementDto>().ReverseMap();
            CreateMap<PayStatementDetailsModel, PayStatementDetailDto>().ReverseMap();
        }
    }

    public static class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
