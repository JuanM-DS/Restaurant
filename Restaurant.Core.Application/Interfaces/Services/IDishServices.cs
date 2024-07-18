using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IDishServices : IGeneralServices<DishDto, Dish>
    {
        public PagedList<DishDto> GetAll(DishQueryFilters filters);

        public PagedList<DishDto> GetAllWithInclude(DishQueryFilters filters);

        public List<DishDto> GetAllWithInclude();

        public Task<DishDto> GetByIdWithIncludeAsync(int id);
    }
}

