using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        public IEnumerable<Ingredient> GetAllWithFilter(IngredientQueryFilters filters);

        public IEnumerable<Ingredient> GetWithInclude(IngredientQueryFilters filters, params Expression<Func<Ingredient, object>>[] properties);

    }
}
