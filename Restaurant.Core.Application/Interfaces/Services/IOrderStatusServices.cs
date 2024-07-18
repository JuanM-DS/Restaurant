using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IOrderStatusServices : IGeneralServices<OrderStatusDto, OrderStatus>
    {
        public List<OrderStatusDto> GetAll(OrderStatusQueryFilters filters);

        public List<OrderStatusDto> GetAllWithInclude(OrderStatusQueryFilters filters);

        public List<OrderStatusDto> GetAllWithInclude();

        public Task<OrderStatusDto> GetByIdWithIncludeAsync(int id);
    }
}

