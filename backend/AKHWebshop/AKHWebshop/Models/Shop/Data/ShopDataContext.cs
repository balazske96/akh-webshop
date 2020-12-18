using System.Collections.Generic;
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

        public ShopDataContext(DbContextOptions<ShopDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(prod => prod.Id);
            modelBuilder.Entity<Product>().HasIndex(product => product.DisplayName).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(product => product.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(product => product.ImageName).IsUnique();
            modelBuilder.Entity<Product>().Property(product => product.Status).HasDefaultValue(ProductStatus.Hidden);

            modelBuilder.Entity<SizeRecord>().Property(size => size.Size).HasDefaultValue(Size.UNDEFINED);
            modelBuilder.Entity<SizeRecord>().Property(size => size.Size).HasConversion<string>();
            modelBuilder.Entity<SizeRecord>().HasKey(size => new {size.ProductId, size.Size});

            modelBuilder.Entity<Order>().HasKey(order => order.Id);
            modelBuilder.Entity<Order>().Property(order => order.Country).HasDefaultValue("Magyarország");
            modelBuilder.Entity<Order>().Property(order => order.Paid).HasDefaultValue(false);
            modelBuilder.Entity<Order>().Property(order => order.Shipped).HasDefaultValue(false);
            modelBuilder.Entity<Order>().Property(order => order.PublicSpaceType).HasDefaultValue(PublicSpaceType.Utca);

            modelBuilder.Entity<OrderItem>()
                .HasKey(orderItem => new {orderItem.OrderId, orderItem.ProductId, orderItem.Size});


            base.OnModelCreating(modelBuilder);
        }
    }
}