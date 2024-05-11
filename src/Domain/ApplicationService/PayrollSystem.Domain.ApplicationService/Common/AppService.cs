using AutoMapper;
using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Contracts.Dtos.Bases;
using PayrollSystem.Domain.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.ApplicationService.Common
{
    public class AppService<TEntity, TDto, TId> : IAppService<TEntity, TDto>
        where TEntity : IAggregateRoot<TId>
        where TDto : class
          where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {

        private readonly IBaseRepository<TEntity, TId> baseRepository;
        private readonly IMapper _mapper;

        public AppService(IBaseRepository<TEntity, TId> baseRepository, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TDto Create(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            baseRepository.Insert(entity);
            return dto;
        }

        public Task<TDto> CreateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TDto> GetAll()
        {
            var entity = baseRepository.GetAll();
            return _mapper.Map<IEnumerable<TDto>>(entity);
        }

        public Task<IEnumerable<TDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TDto GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TDto Update(Guid id, TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
