using Restaurant.Core.Application.Interfaces.Core.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;

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
    }
}
