using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(RestaurantDbContext context)
            : base(context)
        {}

        public IEnumerable<Order> GetAllWithFilter(OrderQueryFilters filters)
        {
            IQueryable<Order> query = _entity;

            if (filters.Subtotal is not null)
                query = query.Where(x => x.Subtotal == filters.Subtotal);

            if (filters.UserId is not null)
                query = query.Where(x => x.UserId == filters.UserId);

            if (filters.TableId is not null)
                query = query.Where(x => x.TableId == filters.TableId);

            if (filters.StatusId is not null)
                query = query.Where(x => x.StatusId == filters.StatusId);

            return query.AsEnumerable();
        }

        public IEnumerable<Order> GetWithInclude(OrderQueryFilters filters, params Expression<Func<Order, object>>[] properties)
        {
            IQueryable<Order> query = _entity;

            if (filters.Subtotal is not null)
                query = query.Where(x => x.Subtotal == filters.Subtotal);

            if (filters.UserId is not null)
                query = query.Where(x => x.UserId == filters.UserId);

            if (filters.TableId is not null)
                query = query.Where(x => x.TableId == filters.TableId);

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
