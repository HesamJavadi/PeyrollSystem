using AutoMapper;
using Microsoft.AspNetCore.Http;
using PayrollSystem.Domain.ApplicationService.Common;
using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Contracts.Data.Management.Setting;
using PayrollSystem.Domain.Contracts.Data.Management.WebServiceManagement;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Contracts.Dtos.Management.Setting;
using PayrollSystem.Domain.Contracts.InfraService;
using PayrollSystem.Domain.Contracts.Request.Setting;
using PayrollSystem.Domain.Contracts.Service.Management.Setting;
using PayrollSystem.Domain.Core.Entities.Common;
using PayrollSystem.Domain.Core.Entities.Management.Setting;
using PayrollSystem.Domain.Core.Entities.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Management.Setting
{
    public class SettingService : AppService<SettingModel, SettingDto, SettingRequest, int>, ISettingService
    {
        private readonly ISettingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUploadFileHandlerService _fileHandlerService;

        public SettingService(ISettingRepository repository, IMapper mapper, IUploadFileHandlerService fileHandlerService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _fileHandlerService = fileHandlerService;
        }

        public async Task CreateOrUpdateAsync(int id, SettingRequest request)
        {
               await UpdateAsync(id,request);
        }

        public SettingDto GetDefaultSetting()
        {
            var entity = _repository.GetDefaultSetting();
            var dto = _mapper.Map<SettingDto>(entity);
            return dto;
        }

        public override async Task<SettingDto> UpdateAsync(int id, SettingRequest request)
        {
            var entity = new SettingModel();

            if (request.Logo != null)
            {
                var logoPath = _fileHandlerService.ReturnFilePath("files/images", request.Logo.FileName);
                entity.Logo = logoPath;
                await _fileHandlerService.SaveFileAsync(request.Logo, "files\\images");
            }
            else
            {
                var dt = GetById(id);
                entity.Logo = dt.Logo;
            }

            entity.ApplicationName = request.ApplicationName;
            entity.DashboadDescription = request.DashboadDescription;
            await _repository.UpdateAsync(id, entity);

            var model = GetById(id);

            return model;
        }
    }
}
