using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context.EntitiesConfigurations
{
    public class DishCategoryConfigurations : IEntityTypeConfiguration<DishCategory>
    {
        public void Configure(EntityTypeBuilder<DishCategory> builder)
        {
            builder.ToTable("DishCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name);

            #region Auditable
            builder.Property(x => x.CreateBy);
            builder.Property(x => x.CreateTime)
                .HasColumnType("datetime");
            builder.Property(x => x.LastModifiedBy);
            builder.Property(x => x.LastModifiedTime)
                .HasColumnType("datetime");
            #endregion

            #region seeds
            builder.HasData
            (
                new DishCategory()
                {
                    Name = "Appetizer",
                },
                new DishCategory()
                {
                    Name = "Main Course",
                },
                new DishCategory()
                {
                    Name = "Dessert",
                },
                new DishCategory()
                {
                    Name = "Beverage",
                }
            );
            #endregion
        }
    }
}
