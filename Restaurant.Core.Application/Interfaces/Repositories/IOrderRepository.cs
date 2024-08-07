﻿using Restaurant.Core.Application.QueryFilters;
using Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurant.Core.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IEnumerable<Order> GetAllWithFilter(OrderQueryFilters filters);

        public IEnumerable<Order> GetWithInclude(OrderQueryFilters filters, params Expression<Func<Order, object>>[] properties);

        public IEnumerable<Order> GetByTableId(int tableId);
    }
}
