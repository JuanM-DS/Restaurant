using AutoMapper;
using Microsoft.Extensions.Options;
using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.Interfaces.Repositories;
using Restaurant.Core.Application.Interfaces.Services;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Domain.Settings;

namespace Restaurant.Core.Application.Services
{
    public class OrderServices : GenericServices<OrderDto, Order>, IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderServices(IOrderRepository orderRepository, IMapper mapper, IOptions<PaginationSettings> paginationSettings)
            : base(orderRepository, mapper, paginationSettings)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public PagedList<OrderDto> GetAll(OrderQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var orders = _orderRepository.GetAllWithFilter(filters);

            var source = _mapper.Map<List<OrderDto>>(orders);

            return PagedList<OrderDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
        }

        public PagedList<OrderDto> GetAllWithInclude(OrderQueryFilters filters)
        {
            filters.Page = (filters.Page is null) ? _paginationSettings.DefaultPage : filters.Page;
            filters.PageSize = (filters.PageSize is null) ? _paginationSettings.DefaultPageSize : filters.Page;

            var orders = _orderRepository.GetWithInclude(filters);

            var source = _mapper.Map<List<OrderDto>>(orders);

            return PagedList<OrderDto>.Create(source, filters.Page.Value, filters.PageSize.Value);
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

        public override Task<OrderDto> CreateAsync(OrderDto entityDto)
        {
            const int InprogressId = 1;

            entityDto.StatusId = InprogressId;

            return base.CreateAsync(entityDto);
        }
    }
}
