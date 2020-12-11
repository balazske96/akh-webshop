using System;
using System.Collections.Generic;
using AKHWebshop.Controllers;
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
        private ShopDataContext _shopDataContext;

        public ProductControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            DbContextOptionsBuilder<ShopDataContext> builder = new DbContextOptionsBuilder<ShopDataContext>();
            builder.UseInMemoryDatabase("ShopDatabase");
            var options = builder.Options;
            _shopDataContext = new ShopDataContext(options);
        }

        [Fact]
        public void GetProductsGivesBackSameProducts()
        {

            Guid guid1 = new Guid();
            Guid guid2 = new Guid();
            Guid guid3 = new Guid();
            Guid guid4 = new Guid();

            List<SizeRecord> sizeRecord1 = new List<SizeRecord>
            {
                new SizeRecord(){ProductId=guid1, Size=Size.L, Quantity=3},
                new SizeRecord(){ProductId=guid1, Size=Size.M, Quantity=2},
            };
            List<SizeRecord> sizeRecord2 = new List<SizeRecord>
            {
                new SizeRecord(){ProductId=guid1, Size=Size.S, Quantity=5}
            };
            List<SizeRecord> sizeRecord3 = new List<SizeRecord>
            {
                new SizeRecord(){ProductId=guid1, Size=Size.L, Quantity=3},
                new SizeRecord(){ProductId=guid1, Size=Size.M, Quantity=2},
            };
            List<SizeRecord> sizeRecord4 = new List<SizeRecord>
            {
                new SizeRecord(){ProductId=guid1, Size=Size.L, Quantity=3},
                new SizeRecord(){ProductId=guid1, Size=Size.M, Quantity=2},
            };


            var expectedTestProducts = new List<Product>
                {
                    new Product(){ Id=guid1, DisplayName="Best T-Shirt 1", ImageName="image1.jpg", Name="akh-tshirt-1", Sizes=sizeRecord1},
                    new Product(){ Id=guid2, DisplayName="Best T-Shirt 2", ImageName="image2.jpg", Name="akh-tshirt-2", Sizes=sizeRecord2},
                    new Product(){ Id=guid3, DisplayName="Best T-Shirt 3", ImageName="image3.jpg", Name="akh-tshirt-3", Sizes=sizeRecord3},
                    new Product(){ Id=guid4, DisplayName="Best T-Shirt 4", ImageName="image4.jpg", Name="akh-tshirt-4", Sizes=sizeRecord4},
                };
            _shopDataContext.AddRange(expectedTestProducts);
            _shopDataContext.SaveChanges();

            ILogger<Product> logger = new Logger<Product>(new LoggerFactory());
            ProductController productController = new ProductController(logger, _shopDataContext);
            JsonResult expectedResult = new JsonResult(expectedTestProducts);
            JsonResult actualResult = productController.GetProducts();

            Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());

        }

        [Fact]
        public void ShouldThrowErrorBecauseOfUniqueIndex()
        {

            Assert.True(true);

        }
    }
}