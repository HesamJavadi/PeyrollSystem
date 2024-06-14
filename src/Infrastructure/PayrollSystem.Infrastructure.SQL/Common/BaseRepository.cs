using Microsoft.EntityFrameworkCore;
using PayrollSystem.Domain.Contracts.Common;
using PayrollSystem.Domain.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Any(expression);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }

        public virtual TEntity Get(TId id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual void Update(TId id, TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.ID = id;
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task UpdateAsync(TId id, TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (existingEntity == null)
                throw new InvalidOperationException($"Entity with id {id} not found.");

            entity.ID = id;
            // Update the existing entity with the new values
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
