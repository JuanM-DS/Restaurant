using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Entities;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class DishCategoryServices : GenericServices<DishCategoryDto, DishCategory>, IDishCategoryServices
    {
        private readonly IDishCategoryRepository _dishCategoryRepository;
        private readonly IMapper _mapper;

        public DishCategoryServices(IDishCategoryRepository dishCategoryRepository, IMapper mapper)
            : base(dishCategoryRepository, mapper)
        {
            _dishCategoryRepository = dishCategoryRepository;
            _mapper = mapper;
        }

        public override async Task<DishCategoryDto> CreateAsync(DishCategoryDto entityDto)
        {
            var dishCategoryByName = await _dishCategoryRepository.GetByNameAsync(entityDto.Name);
            if (dishCategoryByName is not null)
                throw new Exceptions.RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto);
        }

        public override async Task UpdateAsync(int entityDtoId, DishCategoryDto entityDto)
        {
            var dishCategoryByName = await _dishCategoryRepository.GetByNameAsync(entityDto.Name);
            if (dishCategoryByName is not null && dishCategoryByName.Id != entityDtoId)
                throw new Exceptions.RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto);
        }
    }
}
