using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context.EntitiesConfigurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Subtotal);
            builder.Property(x => x.UserId);
            builder.Property(x => x.TableId);
            builder.Property(x => x.StatusId);

            #region Auditable
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.CreatedTime)
                .HasColumnType("datetime");
            builder.Property(x => x.LastModifiedBy);
            builder.Property(x => x.LastModifiedTime)
                .HasColumnType("datetime");
            #endregion

            builder.HasOne(x => x.Table)
                .WithMany(f => f.Orders)
                .HasForeignKey(x => x.TableId);

            builder.HasOne(x => x.Status)
                .WithMany(f => f.Orders)
                .HasForeignKey(x => x.StatusId);
        }
    }
}
