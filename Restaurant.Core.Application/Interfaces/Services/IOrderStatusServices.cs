using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IOrderStatusServices : IGeneralServices<OrderStatusDto, OrderStatus>
    {
        public PagedList<OrderStatusDto> GetAll(OrderStatusQueryFilters filters);
    }
}

