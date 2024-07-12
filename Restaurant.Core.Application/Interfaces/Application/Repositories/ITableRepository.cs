using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        public IEnumerable<Table> GetAllWithFilter(TableQueryFilters filters);
    }
}
