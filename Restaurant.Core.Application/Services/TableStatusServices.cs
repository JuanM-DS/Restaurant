using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Exceptions;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Domain.Entities;
using System.Net;

namespace Restaurant.Core.Application.Services
{
    public class TableStatusServices : GenericServices<TableStatusDto, TableStatus>, ITableStatusServices
    {
        private readonly ITableStatusRepository _tableStatusRepository;
        private readonly IMapper _mapper;

        public TableStatusServices(ITableStatusRepository tableStatusRepository, IMapper mapper)
            : base(tableStatusRepository, mapper)
        {
            _tableStatusRepository = tableStatusRepository;
            _mapper = mapper;
        }

        public override async Task<TableStatusDto> CreateAsync(TableStatusDto entityDto)
        {
            var tableStatusByName = await _tableStatusRepository.GetByNameAsync(entityDto.Name);
            if (tableStatusByName is not null)
                throw new Exceptions.RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto);
        }

        public override async Task UpdateAsync(int entityDtoId, TableStatusDto entityDto)
        {
            var tableStatusByName = await _tableStatusRepository.GetByNameAsync(entityDto.Name);
            if (tableStatusByName is not null && tableStatusByName.Id != entityDtoId)
                throw new Exceptions.RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto);
        }
    }
}
