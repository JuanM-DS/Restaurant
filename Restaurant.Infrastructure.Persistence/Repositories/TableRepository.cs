using Restaurant.Core.Application.Interfaces.Core.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        public TableRepository(RestaurantDbContext context)
            : base(context)
        {}

        public IEnumerable<Table> GetAllWithFilter(TableQueryFilters filters)
        {
            IQueryable<Table> query = _entity;

            if (filters.Guests is not null)
                query = query.Where(x => x.Guests == filters.Guests);

            if (filters.StatusId is not null)
                query = query.Where(x => x.StatusId == filters.StatusId);

            return query.AsEnumerable();
        }
    }
}
