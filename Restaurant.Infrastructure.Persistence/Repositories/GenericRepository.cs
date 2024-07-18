using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Domain.Common;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity>(RestaurantDbContext context) 
        : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly RestaurantDbContext _context = context;
        protected readonly DbSet<TEntity> _entity = context.Set<TEntity>();

        public async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                _entity.Add(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _entity.Remove(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.AsEnumerable();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] properties)
        {
            IQueryable<TEntity> query = _entity;

            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return query.AsEnumerable();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<TEntity?> GetWithIncludeAsync(int id, params Expression<Func<TEntity, object>>[] properties)
        {
            IQueryable<TEntity> query = _entity;

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            var entity = await query.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _entity.Update(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
