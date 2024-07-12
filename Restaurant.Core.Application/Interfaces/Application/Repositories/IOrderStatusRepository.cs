using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface IOrderStatusRepository : IGenericRepository<OrderStatus>
    {
        public IEnumerable<OrderStatus> GetAllWithFilter(OrderStatusQueryFilters filters);
    }
}
