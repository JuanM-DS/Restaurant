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
    public class TableServices : GenericServices<TableDto, Table>, ITableServices
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;
        private readonly ITableStatusRepository _tableStatusRepository;

        public TableServices(ITableRepository tableRepository, IMapper mapper, ITableStatusRepository tableStatusRepository)
            : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _tableStatusRepository = tableStatusRepository;
        }

        public async Task ChangeStatus(int tableId, int tableStatusId)
        {
            var tableById = await _tableRepository.GetByIdAsync(tableId);
            if (tableById is null)
                throw new RestaurantException($"There is not any table with this Id: {tableId}", HttpStatusCode.NoContent);

            var tableStatusById = await _tableStatusRepository.GetByIdAsync(tableStatusId);
            if (tableStatusById is null)
                throw new RestaurantException($"There is not any table status with this Id: {tableStatusId}", HttpStatusCode.NoContent);
            
            tableById.Status = tableStatusById;
            var result = await _tableRepository.UpdateAsync(tableById);
            if (result)
                throw new RestaurantException($"There is a error while updating the table", HttpStatusCode.BadRequest);
        }

        public override async Task DeleteAsync(int entityDtoId)
        {
            var tableStatus = await _tableRepository.GetStatusOfTableById(entityDtoId);
            if (tableStatus != "Available")
                throw new RestaurantException("To delete a tableById it must be available", HttpStatusCode.BadRequest);

            await base.DeleteAsync(entityDtoId);
        }

        public List<TableDto> GetAll(TableQueryFilters filters)
        {
            var tables = _tableRepository.GetAllWithFilter(filters);
            return _mapper.Map<List<TableDto>>(tables);
        }

        public async Task<List<OrderDto>> GetTableOrderInProcess(int tableId)
        {
            const int InprogressId = 1;

            var tableById = await _tableRepository.GetByIdWithIncludeAsync(tableId);
            if (tableById is null)
                throw new RestaurantException($"There is not any table with this Id: {tableId}", HttpStatusCode.NoContent);

            var orders = tableById.Orders.Where(x=>x.StatusId == InprogressId);

            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
