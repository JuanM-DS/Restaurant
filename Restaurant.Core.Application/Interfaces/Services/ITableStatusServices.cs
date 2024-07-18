using Restaurant.Core.Application.DTOs.Entities;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface ITableStatusServices : IGeneralServices<TableStatusDto, TableStatus>
    {
        public List<TableStatusDto> GetAll(TableStatusQueryFilters filters);
    }
}

