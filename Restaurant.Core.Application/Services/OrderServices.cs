using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services
{
    public class OrderServices : GenericServices<OrderDto, Order>, IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderServices(IOrderRepository orderRepository, IMapper mapper)
            : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public List<OrderDto> GetAll(OrderQueryFilters filters)
        {
            var orders = _orderRepository.GetAllWithFilter(filters);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public List<OrderDto> GetAllWithInclude(OrderQueryFilters filters)
        {
            var orders = _orderRepository.GetWithInclude(filters, x=>x.SelectedDishes);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public List<OrderDto> GetAllWithInclude()
        {
            var orders = _orderRepository.GetWithInclude(x => x.SelectedDishes);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdWithIncludeAsync(int id)
        {
            var orders = await _orderRepository.GetByIdWithIncludeAsync(id ,x => x.SelectedDishes);
            return _mapper.Map<OrderDto>(orders);
        }
    }
}
