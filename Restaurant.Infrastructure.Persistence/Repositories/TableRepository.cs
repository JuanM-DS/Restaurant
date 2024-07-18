using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

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

        public async Task<string?> GetStatusOfTableById(int tableId)
        {
            var table = await _entity.Include(x=>x.Status).FirstOrDefaultAsync(x=>x.Id == tableId);
            return table is not null ? table.Status.Name : null;
        }

        public IEnumerable<Table> GetWithInclude(TableQueryFilters filters, params Expression<Func<Table, object>>[] properties)
        {
            IQueryable<Table> query = _entity;

            if (filters.Guests is not null)
                query = query.Where(x => x.Guests == filters.Guests);

            if (filters.StatusId is not null)
                query = query.Where(x => x.StatusId == filters.StatusId);

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return query.AsEnumerable();
        }
    }
}
