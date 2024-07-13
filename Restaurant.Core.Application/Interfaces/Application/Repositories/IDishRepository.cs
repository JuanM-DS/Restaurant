using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        public IEnumerable<Dish> GetAllWithFilter(DishQueryFilters filters);

        public IEnumerable<Dish> GetWithInclude(DishQueryFilters filters, params Expression<Func<Dish, object>>[] properties);

    }
}
