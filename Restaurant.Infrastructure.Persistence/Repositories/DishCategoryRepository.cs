using Restaurant.Core.Application.Interfaces.Core.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Infrastructure.Persistence.Context;

namespace Restaurant.Infrastructure.Persistence.Repositories
{
    public class DishCategoryRepository : GenericRepository<DishCategory>, IDishCategoryRepository
    {
        public DishCategoryRepository(RestaurantDbContext context)
            : base(context)
        {}

        public IEnumerable<DishCategory> GetAll(DishCategoryQueryFilters filters)
        {
            IQueryable<DishCategory> query = _entity;

            if(filters.Name is not null) 
                query = query.Where(x => x.Name == filters.Name);

            return query.AsEnumerable();
        }
    }
}
