using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace AKHWebshop.Models.Shop.Data
{
    public class ShopDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ShopDataContext(DbContextOptions<ShopDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(prod => prod.Id);
            modelBuilder.Entity<Product>().HasIndex(product => product.Id).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(product => product.DisplayName).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(product => product.ImageName).IsUnique();

            modelBuilder.Entity<SizeRecord>().Property(size => size.Size).HasDefaultValue(Size.UNDEFINED);
            modelBuilder.Entity<SizeRecord>().Property(size => size.Size).HasConversion<string>();
            
            modelBuilder.Entity<SizeRecord>().HasKey(size => new {size.ProductId, size.Size});

            base.OnModelCreating(modelBuilder);
        }
    }
}