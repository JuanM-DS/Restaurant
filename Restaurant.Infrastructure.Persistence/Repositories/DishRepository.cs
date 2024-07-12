using Restaurant.Core.Application.Interfaces.Core.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;

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
    }
}
