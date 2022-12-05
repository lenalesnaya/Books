using System;
using System.Threading.Tasks;

namespace App.Common.Repositories.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        Task SaveChangesAsync();
    }
}