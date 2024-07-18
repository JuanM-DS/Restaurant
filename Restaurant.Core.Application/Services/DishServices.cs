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
    public class DishServices : GenericServices<DishDto, Dish>, IDishServices
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public DishServices(IDishRepository dishRepository, IMapper mapper)
            : base(dishRepository, mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public override async Task<DishDto> CreateAsync(DishDto entityDto)
        {
            var dishByName = await _dishRepository.GetByNameAsync(entityDto.Name);
            if (dishByName is not null)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto);
        }

        public List<DishDto> GetAll(DishQueryFilters filters)
        {
            var dishes = _dishRepository.GetAllWithFilter(filters);
            return _mapper.Map<List<DishDto>>(dishes);
        }

        public List<DishDto> GetAllWithInclude(DishQueryFilters filters)
        {
            var dishes = _dishRepository.GetWithInclude(filters, x=>x.Ingredients);
            return _mapper.Map<List<DishDto>>(dishes);
        }

        public List<DishDto> GetAllWithInclude()
        {
            var dishes = _dishRepository.GetWithInclude(x => x.Ingredients);
            return _mapper.Map<List<DishDto>>(dishes);
        }

        public async Task<DishDto> GetByIdWithIncludeAsync(int id)
        {
            var dishes = await _dishRepository.GetByIdWithIncludeAsync(id, x => x.Ingredients);
            return _mapper.Map<DishDto>(dishes);
        }

        public override async Task UpdateAsync(int entityDtoId, DishDto entityDto)
        {
            var dishByName = await _dishRepository.GetByNameAsync(entityDto.Name);
            if (dishByName is not null && dishByName.Id != entityDtoId)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto);
        }
    }
}
