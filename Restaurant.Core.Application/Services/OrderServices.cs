using AutoMapper;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
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
    }
}
