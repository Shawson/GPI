using GPI.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GPI.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAllAsync();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task SaveChanges();
    }
}
