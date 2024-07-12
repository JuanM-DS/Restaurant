using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        public IEnumerable<Ingredient> GetAllWithFilter(IngredientQueryFilters filters);
    }
}
