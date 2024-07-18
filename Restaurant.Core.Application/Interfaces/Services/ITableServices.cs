using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface ITableServices : IGeneralServices<TableDto, Table>
    {
        public List<TableDto> GetAll(TableQueryFilters filters);

        public List<TableDto> GetAllWithInclude(TableQueryFilters filters);

        public List<TableDto> GetAllWithInclude();

        public Task<TableDto> GetByIdWithIncludeAsync(int id);
    }
}

