using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class IngredientServices : GenericServices<IngredientDto, Ingredient>, IIngredientServices
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientServices(IIngredientRepository ingredientRepository, IMapper mapper)
            : base(ingredientRepository, mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public override async Task<IngredientDto> CreateAsync(IngredientDto entityDto)
        {
            var ingredientByName = await _ingredientRepository.GetByNameAsync(entityDto.Name);
            if (ingredientByName is not null)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto); 
        }

        public List<IngredientDto> GetAll(IngredientQueryFilters filters)
        {
            var ingredients = _ingredientRepository.GetAllWithFilter(filters);
            return _mapper.Map<List<IngredientDto>>(ingredients);
        }

        public override async Task UpdateAsync(int entityDtoId, IngredientDto entityDto)
        {
            var ingredientByName = await _ingredientRepository.GetByNameAsync(entityDto.Name);
            if (ingredientByName is not null && ingredientByName.Id != entityDtoId)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto); 
        }
    }
}
