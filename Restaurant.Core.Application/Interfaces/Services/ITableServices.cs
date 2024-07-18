using Restaurant.Core.Application.CustomEntities;
using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface ITableServices : IGeneralServices<TableDto, Table>
    {
        public PagedList<TableDto> GetAll(TableQueryFilters filters);

        public List<OrderDto> GetTableOrderInProcess(int tableId);

        public PagedList<OrderDto> GetTableOrderInProcess(int tableId, OrderQueryFilters filters);

        public Task ChangeStatusAsync(int tableId, int tableStatusId);
    }
}

