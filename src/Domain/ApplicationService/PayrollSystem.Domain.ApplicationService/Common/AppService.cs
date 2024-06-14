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
    public class AppService<TEntity, TDto, TRequest, TId> : IAppService<TEntity, TDto, TRequest, TId>
        where TEntity : IAggregateRoot<TId>
        where TDto : class
        where TRequest : class
          where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {

        private readonly IBaseRepository<TEntity, TId> _baseRepository;
        private readonly IMapper _mapper;

        public AppService(IBaseRepository<TEntity, TId> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual TDto Create(TRequest dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _baseRepository.Insert(entity);
            return _mapper.Map<TDto>(entity);
        }

        public virtual Task<TDto> CreateAsync(TRequest dto)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var entity = _baseRepository.GetAll();
            return _mapper.Map<IEnumerable<TDto>>(entity);
        }

        public virtual Task<IEnumerable<TDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual TDto GetById(TId id)
        {
            var entity = _baseRepository.Get(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual Task<TDto> GetByIdAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public virtual TDto Update(TId id, TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            _baseRepository.Update(id, entity);
            return _mapper.Map<TDto>(request);
        }

        public virtual async Task<TDto> UpdateAsync(TId id, TRequest dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _baseRepository.UpdateAsync(id,entity);
            return _mapper.Map<TDto>(dto);
        }
    }
}
