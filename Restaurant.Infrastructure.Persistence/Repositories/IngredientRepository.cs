using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Core.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RestaurantDbContext context)
            : base (context)
        {}

        public IEnumerable<Ingredient> GetAllWithFilter(IngredientQueryFilters filters)
        {
            IQueryable<Ingredient> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            return query.AsEnumerable();
        }

        public IEnumerable<Ingredient> GetWithInclude(IngredientQueryFilters filters, params Expression<Func<Ingredient, object>>[] properties)
        {
            IQueryable<Ingredient> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return query.AsEnumerable();
        }
    }
}
