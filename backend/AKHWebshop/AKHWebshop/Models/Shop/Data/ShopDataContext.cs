using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


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
            modelBuilder.Entity<Product>().HasMany(product => product.Sizes);

            modelBuilder.Entity<SizeRecord>().HasKey(size => new { size.ProductId, size.Size });

            base.OnModelCreating(modelBuilder);
        }
    }
}