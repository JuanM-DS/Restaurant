using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(RestaurantDbContext context)
            : base (context)
        {}

        public IEnumerable<OrderStatus> GetAllWithFilter(OrderStatusQueryFilters filters)
        {
            IQueryable<OrderStatus> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            return query.AsEnumerable();
        }

        public async Task<OrderStatus?> GetByNameAsync(string name)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Name == name);
        }

        public IEnumerable<OrderStatus> GetWithInclude(OrderStatusQueryFilters filters, params Expression<Func<OrderStatus, object>>[] properties)
        {
            IQueryable<OrderStatus> query = _entity;

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
