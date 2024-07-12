using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface IDishCategoryRepository : IGenericRepository<DishCategory>
    {
        public IEnumerable<DishCategory> GetAll(DishCategoryQueryFilters filters);
    }
}
