using PayrollSystem.Domain.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Common
{
    public interface IBaseRepository<TEntity, TId>
          where TEntity : IAggregateRoot<TId>
          where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {
        List<TEntity> GetAll();
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TId id,TEntity entity);
        Task UpdateAsync(TId id,TEntity entity);
        TEntity Get(TId id);
        Task<TEntity> GetAsync(TId id);
        void Delete(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

    }
}
