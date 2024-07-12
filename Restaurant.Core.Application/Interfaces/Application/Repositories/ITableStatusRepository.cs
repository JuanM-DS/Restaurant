using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface ITableStatusRepository : IGenericRepository<TableStatus>
    {
        public IEnumerable<TableStatus> GetAllWithFilter(TableStatusQueryFilters filters);
    }
}
