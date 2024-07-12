using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IEnumerable<Order> GetAllWithFilter(OrderQueryFilters filters);
    }
}
