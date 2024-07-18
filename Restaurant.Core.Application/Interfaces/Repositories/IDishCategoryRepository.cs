using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IDishCategoryRepository : IGenericRepository<DishCategory>
    {
        public IEnumerable<DishCategory> GetAll(DishCategoryQueryFilters filters);

        public IEnumerable<DishCategory> GetWithInclude(DishCategoryQueryFilters filters, params Expression<Func<DishCategory, object>>[] properties);

        public Task<DishCategory?> GetByNameAsync(string name);
    }
}
