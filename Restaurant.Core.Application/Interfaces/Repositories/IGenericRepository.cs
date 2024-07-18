using Restaurant.Core.Domain.Common;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> 
        where TEntity : BaseEntity
    {
        public Task<bool> CreateAsync(TEntity entity);

        public Task<bool> UpdateAsync(TEntity entity);

        public Task<bool> DeleteAsync(TEntity entity);

        public Task<TEntity?> GetByIdAsync(int id);

        public IEnumerable<TEntity> GetAll();

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] properties);

        public Task<TEntity?> GetWithIncludeAsync(int id, params Expression<Func<TEntity, object>>[] properties);
    }
}
