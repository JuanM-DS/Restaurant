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
    public class OrderStatusServices : GenericServices<OrderStatusDto, OrderStatus>, IOrderStatusServices
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusServices(IOrderStatusRepository orderStatusRepository, IMapper mapper)
            : base(orderStatusRepository, mapper)
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

        public List<OrderStatusDto> GetAll(OrderStatusQueryFilters filters)
        {
            var orderStatus = _orderStatusRepository.GetAllWithFilter(filters);
            return _mapper.Map<List<OrderStatusDto>>(orderStatus);
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
