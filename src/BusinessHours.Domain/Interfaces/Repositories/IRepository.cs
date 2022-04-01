using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessHours.Domain.Entities;

namespace BusinessHours.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<TEntity> InsertAsync(TEntity data);
        Task<TEntity> UpdateAsync(TEntity data);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<TEntity> SelectAsync(Guid id);
        Task<IEnumerable<TEntity>> SelectAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> SaveChangesAsync();
    }
}
