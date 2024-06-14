using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Common
{
    public interface IAppService<TEntity, TDto, TRequest , TId> 
                  where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {
        IEnumerable<TDto> GetAll();
        Task<IEnumerable<TDto>> GetAllAsync();
        TDto GetById(TId id);
        Task<TDto> GetByIdAsync(TId id);
        TDto Create(TRequest dto);
        Task<TDto> CreateAsync(TRequest dto);
        TDto Update(TId id, TRequest dto);
        Task<TDto> UpdateAsync(TId id, TRequest dto);
        bool Delete(TId id);
        Task<bool> DeleteAsync(TId id);
    }
}
