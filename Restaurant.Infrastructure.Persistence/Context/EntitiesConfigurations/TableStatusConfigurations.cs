using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context.EntitiesConfigurations
{
    public class TableStatusConfigurations : IEntityTypeConfiguration<TableStatus>
    {
        public void Configure(EntityTypeBuilder<TableStatus> builder)
        {
            builder.ToTable("TableStates");

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
                new TableStatus()
                {
                    Id = 1,
                    Name = "Available",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                },
                new TableStatus()
                {
                    Id = 2,
                    Name = "In Progress",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                },
                new TableStatus()
                {
                    Id = 3,
                    Name = "Served",
                    CreatedBy = "System",
                    CreatedTime = DateTime.UtcNow
                }
            );
            #endregion
        }
    }
}
