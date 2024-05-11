using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.SQL.Common
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
         where TEntity : AggregateRoot<TId>
         where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {
        protected readonly ApplicationDbContext _dbContext;

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TId id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
           return _dbContext.Set<TEntity>().ToList();
        }

        public Task<TEntity> GetAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public Task InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
