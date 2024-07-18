using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IIngredientServices : IGeneralServices<IngredientDto, Ingredient>
    {
        public List<IngredientDto> GetAll(IngredientQueryFilters filters);
    }
}

