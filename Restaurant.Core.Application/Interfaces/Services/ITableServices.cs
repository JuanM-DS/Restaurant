using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface ITableServices : IGeneralServices<TableDto, Table>
    {
        public List<TableDto> GetAll(TableQueryFilters filters);

        public Task<List<OrderDto>> GetTableOrderInProcess(int tableId);

        public Task ChangeStatus(int tableId, int tableStatusId);
    }
}

