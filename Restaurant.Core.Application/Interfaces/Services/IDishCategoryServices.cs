using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IDishCategoryServices : IGeneralServices<DishCategoryDto, DishCategory>
    {
        public List<DishCategoryDto> GetAll(DishCategoryQueryFilters filters);

        public List<DishCategoryDto> GetAllWithInclude(DishCategoryQueryFilters filters);

        public List<DishCategoryDto> GetAllWithInclude();

        public Task<DishCategoryDto> GetByIdWithIncludeAsync(int id);
    }
}

