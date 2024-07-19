using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.Identity.Entities;
using System.Reflection.Emit;

namespace Restaurant.Infrastructure.Identity.Context
{
    public class RestaurantIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public RestaurantIdentityDbContext(DbContextOptions<RestaurantIdentityDbContext> option)
            : base(option)
        {}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.Entity.GetType().Name)
                {
                    case nameof(ApplicationUser):
                        var userEntity = (ApplicationUser)entry.Entity;
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                userEntity.LastModifiedBy = "user"; // change by the user
                                userEntity.LastModifiedTime = DateTime.Now;
                                break;
                            case EntityState.Added:
                                userEntity.CreatedTime = DateTime.Now;
                                userEntity.CreatedBy = "user"; // change by the user
                                break;
                        }
                        break;

                    case nameof(ApplicationRole):
                        var roleEntity = (ApplicationRole)entry.Entity;
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                roleEntity.LastModifiedBy = "user"; // change by the user
                                roleEntity.LastModifiedTime = DateTime.Now;
                                break;
                            case EntityState.Added:
                                roleEntity.CreatedTime = DateTime.Now;
                                roleEntity.CreatedBy = "user"; // change by the user
                                break;
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");

            #region entities configurations
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
                
                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd();

                #region Auditable
                entity.Property(x => x.CreatedBy);
                entity.Property(x => x.CreatedTime)
                    .HasColumnType("datetime");
                entity.Property(x => x.LastModifiedBy);
                entity.Property(x => x.LastModifiedTime)
                    .HasColumnType("datetime");
                #endregion

                entity.HasMany(e => e.Roles)
                      .WithMany(f => f.Users)
                      .UsingEntity<IdentityUserRole<string>>();
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles");

                #region Auditable
                entity.Property(x => x.CreatedBy);
                entity.Property(x => x.CreatedTime)
                    .HasColumnType("datetime");
                entity.Property(x => x.LastModifiedBy);
                entity.Property(x => x.LastModifiedTime)
                    .HasColumnType("datetime");
                #endregion
            });
            
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            #endregion
        }
    }
}
