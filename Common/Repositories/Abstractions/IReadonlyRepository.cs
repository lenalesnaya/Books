using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Common.Repositories.Abstractions
{
    public interface IReadonlyRepository<Entity>
        where Entity : class
    {
        Task<IReadOnlyCollection<Entity>> GetAllAsync();

        Task<IReadOnlyCollection<Entity>> GetWhereAsync(Expression<Func<Entity, bool>> filter);

        Task<Entity> GetSingleOrDefaultAsync(Expression<Func<Entity, bool>> filter);

        Task<bool> AnyAsync(Expression<Func<Entity, bool>> filter);
    }
}