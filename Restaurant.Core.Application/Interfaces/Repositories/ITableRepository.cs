﻿using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        public IEnumerable<Table> GetAllWithFilter(TableQueryFilters filters);

        public IEnumerable<Table> GetWithInclude(TableQueryFilters filters, params Expression<Func<Table, object>>[] properties);

        public Task<string?> GetStatusOfTableById(int tableId);
    }
}
