using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        public DishRepository(RestaurantDbContext context)
            : base(context)
        {}

        public IEnumerable<Dish> GetAllWithFilter(DishQueryFilters filters)
        {
            IQueryable<Dish> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            if (filters.Price is not null)
                query = query.Where(x => x.Price == filters.Price);

            if (filters.Portions is not null)
                query = query.Where(x => x.Portions == filters.Portions);

            if (filters.CategoryId is not null)
                query = query.Where(x => x.CategoryId == filters.CategoryId);

            return query.AsEnumerable();
        }

        public async Task<Dish?> GetByNameAsync(string name)
        {
            return await _entity.FirstOrDefaultAsync(x => x.Name == name);
        }

        public override async Task<bool> UpdateAsync(Dish entity)
        {
            var ingredientIds = entity.Ingredients.Select(ei => ei.Id).ToList();

            var existingIngredients = await _context.Ingredients
                                                     .Where(i => ingredientIds.Contains(i.Id))
                                                     .ToListAsync();

            if (existingIngredients.Count != ingredientIds.Count)
            {
                return false;
            }
            
            entity.Ingredients = existingIngredients;

            return await base.UpdateAsync(entity);
        }

        public override async Task<bool> CreateAsync(Dish entity)
        {
            var existingIngredients = _context.Ingredients
                 .Where(c => entity.Ingredients.Select(ei => ei.Id).Contains(c.Id))
                 .ToList();

            if (existingIngredients.Count != entity.Ingredients.Count)
            {
                return false;
            }

            entity.Ingredients = existingIngredients;

            return await base.CreateAsync(entity);
        }

        public IEnumerable<Dish> GetWithInclude(DishQueryFilters filters, params Expression<Func<Dish, object>>[] properties)
        {
            IQueryable<Dish> query = _entity;

            if (filters.Name is not null)
                query = query.Where(x => x.Name == filters.Name);

            if (filters.Price is not null)
                query = query.Where(x => x.Price == filters.Price);

            if (filters.Portions is not null)
                query = query.Where(x => x.Portions == filters.Portions);

            if (filters.CategoryId is not null)
                query = query.Where(x => x.CategoryId == filters.CategoryId);


            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return query.AsEnumerable();
        }
    }
}
