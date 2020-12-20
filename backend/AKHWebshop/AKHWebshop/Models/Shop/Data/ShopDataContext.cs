using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AKHWebshop.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace AKHWebshop.Models.Shop.Data
{
    public class ShopDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SizeRecord> SizeRecords { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public ShopDataContext(DbContextOptions<ShopDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the Product entity
            modelBuilder.Entity<Product>(model =>
            {
                model.HasKey(prod => prod.Id);
                model.HasIndex(product => product.DisplayName).IsUnique();
                model.HasIndex(product => product.Name).IsUnique();
                model.HasIndex(product => product.ImageName).IsUnique();
                model.Property(product => product.Status).HasDefaultValue(ProductStatus.Hidden);
            });

            // Set the SizeRecord entity
            modelBuilder.Entity<SizeRecord>(model =>
            {
                model.HasKey(size => new {size.ProductId, size.Size});
                model.Property(size => size.Size).HasDefaultValue(Size.UNDEFINED);
                model.Property(size => size.Size).HasConversion<string>();
            });

            // Set the Order entity
            modelBuilder.Entity<Order>(model =>
            {
                model.HasKey(order => order.Id);
                model.Property(order => order.Paid).HasDefaultValue(false);
                model.Property(order => order.Shipped).HasDefaultValue(false);
                model.Property(order => order.PublicSpaceType).HasDefaultValue(PublicSpaceType.Utca);
            });

            // Set the BillingInfo entity
            modelBuilder.Entity<BillingInfo>(model =>
            {
                model.ToTable("billing_info");
                model.HasKey(b => b.Id);
            });

            // Set the OrderItem entity
            modelBuilder.Entity<OrderItem>(model =>
            {
                model.HasKey(orderItem => new {orderItem.OrderId, orderItem.ProductId, orderItem.Size});
            });

            // Set the AppUser entity
            modelBuilder.Entity<AppUser>(model =>
            {
                model.ToTable("user");
                model.Property(u => u.Id).HasColumnName("id");
                model.Property(u => u.PasswordHash).HasColumnName("password_hash");
                model.Property(u => u.Email).HasColumnName("email");
                model.Property(u => u.NormalizedEmail).HasColumnName("normalized_email");
                model.Property(u => u.UserName).HasColumnName("username");
                model.Property(u => u.NormalizedUserName).HasColumnName("normalized_username");
                model.Property(u => u.EmailConfirmed).HasColumnName("email_confirmed");
                model.Property(u => u.LockoutEnabled).HasColumnName("lockout_enabled");
                model.Property(u => u.LockoutEnd).HasColumnName("lockout_end");
                model.Property(u => u.PhoneNumber).HasColumnName("phone_number");
                model.Property(u => u.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
                model.Property(u => u.SecurityStamp).HasColumnName("security_stamp");
                model.Property(u => u.ConcurrencyStamp).HasColumnName("concurrency_stamp");
                model.Property(u => u.AccessFailedCount).HasColumnName("access_failed_count");
                model.Property(u => u.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            });

            // Set IdentityRole entity
            modelBuilder.Entity<IdentityRole>(model =>
            {
                model.ToTable("role");
                model.HasData(new List<IdentityRole>()
                {
                    new IdentityRole() {Name = "admin", NormalizedName = "Admin"},
                    new IdentityRole() {Name = "user", NormalizedName = "User"}
                });
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
                cancellationToken));
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that inherit from DatedEntity,
                // set UpdatedAt / CreatedAt appropriately
                if (entry.Entity is DatedEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.UpdatedAt = utcNow;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedAt").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable.CreatedAt = utcNow;
                            trackable.UpdatedAt = utcNow;
                            break;
                    }
                }
            }
        }
    }
}