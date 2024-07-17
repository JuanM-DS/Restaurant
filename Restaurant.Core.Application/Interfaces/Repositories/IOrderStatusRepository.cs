using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IOrderStatusRepository : IGenericRepository<OrderStatus>
    {
        public IEnumerable<OrderStatus> GetAllWithFilter(OrderStatusQueryFilters filters);

        public IEnumerable<OrderStatus> GetWithInclude(OrderStatusQueryFilters filters, params Expression<Func<OrderStatus, object>>[] properties);

    }
}
