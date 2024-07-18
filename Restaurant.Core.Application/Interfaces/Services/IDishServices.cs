using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IDishServices : IGeneralServices<DishDto, Dish>
    {
        public List<DishDto> GetAll(DishQueryFilters filters);

        public List<DishDto> GetAllWithInclude(DishQueryFilters filters);

        public List<DishDto> GetAllWithInclude();

        public Task<DishDto> GetByIdWithIncludeAsync(int id);
    }
}

