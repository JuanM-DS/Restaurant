using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IOrderServices : IGeneralServices<OrderDto, Order>
    {
        public List<OrderDto> GetAll(OrderQueryFilters filters);

        public List<OrderDto> GetAllWithInclude(OrderQueryFilters filters);

        public List<OrderDto> GetAllWithInclude();

        public Task<OrderDto> GetByIdWithIncludeAsync(int id);
    }
}

