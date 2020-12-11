using System;
using System.Collections.Generic;
using AKHWebshop.Controllers;
using AKHWebshop.Models.Shop;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AKHWebshop.Test
{
    public class ProductControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GetProductsGivesBackSameProducts()
        {
            DbContextOptionsBuilder<ShopDataContext> builder = new DbContextOptionsBuilder<ShopDataContext>();
            builder.UseInMemoryDatabase("ShopDatabase");
            var options = builder.Options;
            using (var context = new ShopDataContext(options))
            {
                var products = new List<Product>
                {
                    new Product() {Name = "Póló1"},
                    new Product() {Name = "Póló2"}
                };
                context.AddRange(products);
                context.SaveChanges();
            }

            using (var context = new ShopDataContext(options))
            {
                ILogger<Product> logger = new Logger<Product>(new LoggerFactory());
                ProductController productController = new ProductController(logger, context);
                JsonResult expectedResult = new JsonResult(new List<Product>
                {
                    new Product() {Name = "Póló1", Id = 1},
                    new Product() {Name = "Póló2", Id = 2}
                });
                JsonResult actualResult = productController.GetProducts();

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
            }
        }
    }
}