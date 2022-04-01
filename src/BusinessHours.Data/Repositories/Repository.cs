using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessHours.Data.Contexts;
using BusinessHours.Domain.Entities;
using BusinessHours.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessHours.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AppDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> SelectAsync(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await DbSet.AsNoTracking().AnyAsync(i => i.Id.Equals(id));
        }

        public virtual async Task<TEntity> InsertAsync(TEntity data)
        {
            if (data.Id == string.Empty)
                data.Id = Guid.NewGuid().ToString();
            data.CreatedAt = DateTime.UtcNow;
            DbSet.Add(data);
            await SaveChangesAsync();
            return data;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity data)
        {
            data.UpdatedAt = DateTime.UtcNow;
            DbSet.Update(data);
            await SaveChangesAsync();
            return data;
        }

        public virtual async Task DeleteAsync(string id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var savedItems = await Context.SaveChangesAsync();
            return savedItems > 0;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
