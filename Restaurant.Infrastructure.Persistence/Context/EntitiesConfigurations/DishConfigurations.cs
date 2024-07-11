using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context.EntitiesConfigurations
{
    public class DishConfigurations : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Price)
                   .HasColumnType("decimal");
            builder.Property(x => x.Portions);
            builder.Property(x => x.CategoryId);

            #region Auditable
            builder.Property(x => x.CreateBy);
            builder.Property(x => x.CreateTime)
                .HasColumnType("datetime");
            builder.Property(x => x.LastModifiedBy);
            builder.Property(x => x.LastModifiedTime)
                .HasColumnType("datetime");
            #endregion

            builder.HasOne(x => x.Category)
                .WithMany(f => f.Dishes)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
