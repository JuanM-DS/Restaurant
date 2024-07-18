using AutoMapper;
using Microsoft.Extensions.Options;
using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Domain.Settings;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class DishCategoryServices : GenericServices<DishCategoryDto, DishCategory>, IDishCategoryServices
    {
        private readonly IDishCategoryRepository _dishCategoryRepository;
        private readonly IMapper _mapper;

        public DishCategoryServices(IDishCategoryRepository dishCategoryRepository, IMapper mapper, IOptions<PaginationSettings> paginationSettings)
            : base(dishCategoryRepository, mapper, paginationSettings)
        {
            _dishCategoryRepository = dishCategoryRepository;
            _mapper = mapper;
        }

        public override async Task<DishCategoryDto> CreateAsync(DishCategoryDto entityDto)
        {
            var dishCategoryByName = await _dishCategoryRepository.GetByNameAsync(entityDto.Name);
            if (dishCategoryByName is not null)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto);
        }

        public PagedList<DishCategoryDto> GetAll(DishCategoryQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var dishCategories = _dishCategoryRepository.GetAllWithFilter(filters);

            var source = _mapper.Map<List<DishCategoryDto>>(dishCategories);

            return PagedList<DishCategoryDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public override async Task UpdateAsync(int entityDtoId, DishCategoryDto entityDto)
        {
            var dishCategoryByName = await _dishCategoryRepository.GetByNameAsync(entityDto.Name);
            if (dishCategoryByName is not null && dishCategoryByName.Id != entityDtoId)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto);
        }
    }
}
