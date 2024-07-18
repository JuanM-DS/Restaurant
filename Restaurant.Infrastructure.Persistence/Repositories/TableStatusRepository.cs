using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class TableStatusRepository : GenericRepository<TableStatus>, ITableStatusRepository
    {
        public TableStatusRepository(RestaurantDbContext context)
            : base(context)
        {}
        
        public IEnumerable<TableStatus> GetAllWithFilter(TableStatusQueryFilters filters)
        {
            IQueryable<TableStatus> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            return query.AsEnumerable();
        }

        public async Task<TableStatus?> GetByNameAsync(string name)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Name == name);
        }

        public IEnumerable<TableStatus> GetWithInclude(TableStatusQueryFilters filters, params Expression<Func<TableStatus, object>>[] properties)
        {
            IQueryable<TableStatus> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return query.AsEnumerable();
        }
    }
}
