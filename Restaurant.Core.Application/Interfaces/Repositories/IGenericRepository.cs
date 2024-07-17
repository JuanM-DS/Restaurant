using Restaurant.Core.Domain.Common;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<bool> Create(TEntity entity);

        public Task<bool> Update(TEntity entity);

        public Task<bool> Delete(TEntity entity);

        public Task<TEntity?> GetById(int id);

        public IEnumerable<TEntity> GetAll();

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] properties);

        public Task<TEntity?> GetWithInclude(int id, params Expression<Func<TEntity, object>>[] properties);
    }
}
