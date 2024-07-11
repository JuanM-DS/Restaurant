using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context.EntitiesConfigurations
{
    public class OrderStatusConfigurations : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name);

            #region Auditable
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.CreatedTime)
                .HasColumnType("datetime");
            builder.Property(x => x.LastModifiedBy);
            builder.Property(x => x.LastModifiedTime)
                .HasColumnType("datetime");
            #endregion

            #region seeds
            builder.HasData
            (
                new OrderStatus()
                {
                    Id = 1,
                    Name = "In Progress",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                },
                new OrderStatus()
                {
                    Id = 2,
                    Name = "Completed",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                }
            );
            #endregion
        }
    }
}
