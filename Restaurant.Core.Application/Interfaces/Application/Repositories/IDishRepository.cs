using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        public IEnumerable<Dish> GetAllWithFilter(DishQueryFilters filters);
    }
}
