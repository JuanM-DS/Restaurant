using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Core.Repositories
{
    public interface ITableStatusRepository : IGenericRepository<TableStatus>
    {
        public IEnumerable<TableStatus> GetAllWithFilter(TableStatusQueryFilters filters);

        public IEnumerable<TableStatus> GetWithInclude(TableStatusQueryFilters filters, params Expression<Func<TableStatus, object>>[] properties);
    }
}
