using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Context.EntitiesConfigurations
{
    public class TableConfigurations : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Guests);
            builder.Property(x => x.Description);
            builder.Property(x => x.StatusId);

            #region Auditable
            builder.Property(x => x.CreateBy);
            builder.Property(x => x.CreateTime)
                .HasColumnType("datetime");
            builder.Property(x => x.LastModifiedBy);
            builder.Property(x => x.LastModifiedTime)
                .HasColumnType("datetime");
            #endregion

            builder.HasOne(x => x.Status)
                .WithMany(f => f.Tables)
                .HasForeignKey(x => x.StatusId);
        }
    }
}
