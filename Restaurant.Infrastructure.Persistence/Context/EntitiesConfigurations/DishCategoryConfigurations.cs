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
                new DishCategory()
                {
                    Id = 1,
                    Name = "Appetizer",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                },
                new DishCategory()
                {
                    Id = 2,
                    Name = "Main Course",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                },
                new DishCategory()
                {
                    Id = 3,
                    Name = "Dessert",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                },
                new DishCategory()
                {
                    Id = 4,
                    Name = "Beverage",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                }
            );
            #endregion
        }
    }
}
