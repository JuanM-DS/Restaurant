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
    public class TableServices : GenericServices<TableDto, Table>, ITableServices
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;
        private readonly ITableStatusRepository _tableStatusRepository;
        private readonly IOrderRepository _orderRepository;

        public TableServices(
            ITableRepository tableRepository,
            IMapper mapper,
            ITableStatusRepository tableStatusRepository,
            IOptions<PaginationSettings> paginationSettings,
            IOrderRepository orderRepository
            )
            : base(tableRepository, mapper, paginationSettings)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _tableStatusRepository = tableStatusRepository;
            _orderRepository = orderRepository;
        }

        public async Task ChangeStatusAsync(int tableId, int tableStatusId)
        {
            var tableById = await _tableRepository.GetByIdAsync(tableId);
            if (tableById is null)
                throw new RestaurantException($"There is not any table with this Id: {tableId}", HttpStatusCode.NoContent);

            var tableStatusById = await _tableStatusRepository.GetByIdAsync(tableStatusId);
            if (tableStatusById is null)
                throw new RestaurantException($"There is not any table status with this Id: {tableStatusId}", HttpStatusCode.NoContent);
            
            tableById.Status = tableStatusById;
            tableById.StatusId = tableStatusId;
            var result = await _tableRepository.UpdateAsync(tableById);
            if (!result)
                throw new RestaurantException($"There is a error while updating the table", HttpStatusCode.BadRequest);
        }

        public override async Task DeleteAsync(int entityDtoId)
        {
            var tableStatus = await _tableRepository.GetStatusOfTableById(entityDtoId);
            if (tableStatus != "Available")
                throw new RestaurantException("To delete a tableById it must be available", HttpStatusCode.BadRequest);

            await base.DeleteAsync(entityDtoId);
        }

        public PagedList<TableDto> GetAll(TableQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var tables = _tableRepository.GetAllWithFilter(filters);

            var source = _mapper.Map<List<TableDto>>(tables);

            return PagedList<TableDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public List<OrderDto> GetTableOrderInProcess(int tableId)
        {
            const int InprogressId = 1;

            var orders = _orderRepository.GetByTableId(tableId).Where(x => x.StatusId == InprogressId);

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public PagedList<OrderDto> GetTableOrderInProcess(int tableId, OrderQueryFilters filters)
        {
            const int InprogressId = 1;

            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var orders = _orderRepository.GetByTableId(tableId).Where(x => x.StatusId == InprogressId);

            var source =  _mapper.Map<List<OrderDto>>(orders);

            return PagedList<OrderDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public override Task<TableDto> CreateAsync(TableDto entityDto)
        {
            const int Available = 1;
            entityDto.StatusId = Available;
            return base.CreateAsync(entityDto);
        }
    }
}
