using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Common.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace App.Common.Repositories
{
    public class Repository<Entity> : IRepository<Entity>
        where Entity : class
    {
        private readonly DbSet<Entity> _dbSet;


        public Repository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Entity>();
        }


        public virtual async Task<IReadOnlyCollection<Entity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IReadOnlyCollection<Entity>> GetWhereAsync(Expression<Func<Entity, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public virtual async Task<Entity> GetSingleOrDefaultAsync(Expression<Func<Entity, bool>> filter)
        {
            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<Entity, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public virtual void Add(Entity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(Entity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(Entity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}