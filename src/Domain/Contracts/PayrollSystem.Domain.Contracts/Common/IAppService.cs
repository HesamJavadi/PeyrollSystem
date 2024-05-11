using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Common
{
    public interface IAppService<TEntity, TDto>
    {
        IEnumerable<TDto> GetAll();
        Task<IEnumerable<TDto>> GetAllAsync();
        TDto GetById(Guid id);
        Task<TDto> GetByIdAsync(Guid id);
        TDto Create(TDto dto);
        Task<TDto> CreateAsync(TDto dto);
        TDto Update(Guid id, TDto dto);
        Task<TDto> UpdateAsync(Guid id, TDto dto);
        bool Delete(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
