using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurant.Core.Domain.Common;
using Restaurant.Core.Domain.Entities;
using System.Reflection;

namespace Restaurant.Infrastructure.Persistence.Context
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<DishCategory> DishCategories { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<TableStatus> TableStates { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStates { get; set; }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.LastModifiedBy = "user"; // change by the user
                        item.Entity.LastModifiedTime = DateTime.Now;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedTime = DateTime.Now;
                        item.Entity.CreatedBy = "user"; // change by the user
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
