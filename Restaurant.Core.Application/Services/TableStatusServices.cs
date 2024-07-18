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
    public class TableStatusServices : GenericServices<TableStatusDto, TableStatus>, ITableStatusServices
    {
        private readonly ITableStatusRepository _tableStatusRepository;
        private readonly IMapper _mapper;

        public TableStatusServices(ITableStatusRepository tableStatusRepository, IMapper mapper, IOptions<PaginationSettings> paginationSettings)
            : base(tableStatusRepository, mapper, paginationSettings)
        {
            _tableStatusRepository = tableStatusRepository;
            _mapper = mapper;
        }

        public override async Task<TableStatusDto> CreateAsync(TableStatusDto entityDto)
        {
            var tableStatusByName = await _tableStatusRepository.GetByNameAsync(entityDto.Name);
            if (tableStatusByName is not null)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto);
        }

        public List<TableStatusDto> GetAll(TableStatusQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var tableStatus = _tableStatusRepository.GetAllWithFilter(filters);

            var source = _mapper.Map<List<TableStatusDto>>(tableStatus);

            return PagedList<TableStatusDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public override async Task UpdateAsync(int entityDtoId, TableStatusDto entityDto)
        {
            var tableStatusByName = await _tableStatusRepository.GetByNameAsync(entityDto.Name);
            if (tableStatusByName is not null && tableStatusByName.Id != entityDtoId)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto);
        }
    }
}
