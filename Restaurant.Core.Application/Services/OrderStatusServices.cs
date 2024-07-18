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
    public class OrderStatusServices : GenericServices<OrderStatusDto, OrderStatus>, IOrderStatusServices
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusServices(IOrderStatusRepository orderStatusRepository, IMapper mapper, IOptions<PaginationSettings> paginationSettings)
            : base(orderStatusRepository, mapper, paginationSettings)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        public override async Task<OrderStatusDto> CreateAsync(OrderStatusDto entityDto)
        {
            var orderStatusByName = await _orderStatusRepository.GetByNameAsync(entityDto.Name);
            if (orderStatusByName is not null)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            return await base.CreateAsync(entityDto);
        }

        public PagedList<OrderStatusDto> GetAll(OrderStatusQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var orderStatus = _orderStatusRepository.GetAllWithFilter(filters);

            var source = _mapper.Map<List<OrderStatusDto>>(orderStatus);

            return PagedList<OrderStatusDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public override async Task UpdateAsync(int entityDtoId, OrderStatusDto entityDto)
        {
            var orderStatusByName = await _orderStatusRepository.GetByNameAsync(entityDto.Name);
            if (orderStatusByName is not null && orderStatusByName.Id != entityDtoId)
                throw new RestaurantException($"The name: {entityDto.Name} is already taken", HttpStatusCode.BadRequest);

            await base.UpdateAsync(entityDtoId, entityDto);
        }
    }
}
