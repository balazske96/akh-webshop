using System;
using System.Collections.Generic;
using System.Reflection;
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

        private ProductController CreateTestController()
        {
            ILogger<Product> logger = new Logger<Product>(new LoggerFactory());
            return new ProductController(logger, _shopDataContext);
        }

        [Fact]
        public void GetProductsGivesBackSameProducts()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Guid guid4 = Guid.NewGuid();

            List<SizeRecord> sizeRecord1 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
            };
            List<SizeRecord> sizeRecord2 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid2, Size = Size.S, Quantity = 5}
            };
            List<SizeRecord> sizeRecord3 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid3, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid3, Size = Size.M, Quantity = 2},
            };
            List<SizeRecord> sizeRecord4 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid4, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid4, Size = Size.M, Quantity = 2},
            };


            var expectedTestProducts = new List<Product>
            {
                new Product()
                {
                    Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                    Sizes = sizeRecord1
                },
                new Product()
                {
                    Id = guid2, DisplayName = "Best T-Shirt 2", ImageName = "image2.jpg", Name = "akh-tshirt-2",
                    Sizes = sizeRecord2
                },
                new Product()
                {
                    Id = guid3, DisplayName = "Best T-Shirt 3", ImageName = "image3.jpg", Name = "akh-tshirt-3",
                    Sizes = sizeRecord3
                },
                new Product()
                {
                    Id = guid4, DisplayName = "Best T-Shirt 4", ImageName = "image4.jpg", Name = "akh-tshirt-4",
                    Sizes = sizeRecord4
                },
            };

            _shopDataContext.AddRange(expectedTestProducts);
            _shopDataContext.SaveChanges();

            ProductController productController = this.CreateTestController();

            JsonResult expectedResult = new JsonResult(expectedTestProducts)
            {
                ContentType = "application/json", StatusCode = 200
            };

            JsonResult actualResult = productController.GetProducts(null, null, null);
            Assert.Equal(expectedResult.ToString(), actualResult.ToString());
            Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
            Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
        }

        [Fact]
        public void GeProductsShouldReturnProductWithTheSpecifiedId()
        {
            Guid guid1 = Guid.NewGuid();

            List<SizeRecord> sizeRecord1 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
            };
            var expectedTestProducts =
                new Product()
                {
                    Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                    Sizes = sizeRecord1
                };
            _shopDataContext.Add(expectedTestProducts);

            ProductController productController = CreateTestController();

            JsonResult expectedResult = new JsonResult(expectedTestProducts);
            expectedResult.ContentType = "application/json";
            expectedResult.StatusCode = 200;
            string paramGuid = guid1.ToString();
            JsonResult actualResult = productController.GetProducts(paramGuid, null, null);

            Assert.Equal(expectedResult.ToString(), actualResult.ToString());
            Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
            Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
        }

        [Fact]
        public void GetProductsShouldReturnALimitedSetOfProducts()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Guid guid4 = Guid.NewGuid();

            List<SizeRecord> sizeRecord1 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
            };
            List<SizeRecord> sizeRecord2 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid2, Size = Size.S, Quantity = 5}
            };
            List<SizeRecord> sizeRecord3 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid3, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid3, Size = Size.M, Quantity = 2},
            };
            List<SizeRecord> sizeRecord4 = new List<SizeRecord>
            {
                new SizeRecord() {ProductId = guid4, Size = Size.L, Quantity = 3},
                new SizeRecord() {ProductId = guid4, Size = Size.M, Quantity = 2},
            };


            var expectedTestProducts = new List<Product>
            {
                new Product()
                {
                    Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                    Sizes = sizeRecord1
                },
                new Product()
                {
                    Id = guid2, DisplayName = "Best T-Shirt 2", ImageName = "image2.jpg", Name = "akh-tshirt-2",
                    Sizes = sizeRecord2
                },
                new Product()
                {
                    Id = guid3, DisplayName = "Best T-Shirt 3", ImageName = "image3.jpg", Name = "akh-tshirt-3",
                    Sizes = sizeRecord3
                },
                new Product()
                {
                    Id = guid4, DisplayName = "Best T-Shirt 4", ImageName = "image4.jpg", Name = "akh-tshirt-4",
                    Sizes = sizeRecord4
                },
            };
            _shopDataContext.AddRange(expectedTestProducts);
            _shopDataContext.SaveChanges();

            ProductController productController = CreateTestController();

            JsonResult actualJsonResult1 = productController.GetProducts(null, 2, 2);
            JsonResult actualJsonResult2 = productController.GetProducts(null, null, 3);
            JsonResult actualJsonResult3 = productController.GetProducts(null, 3, 1);
            JsonResult actualJsonResult4 = productController.GetProducts(null, 10, 2);

            Assert.Equal(2, ((List<Product>) actualJsonResult1.Value).Count);
            Assert.Equal(3, ((List<Product>) actualJsonResult2.Value).Count);
            Assert.Single(((List<Product>) actualJsonResult3.Value));
            Assert.Empty((List<Product>) actualJsonResult4.Value);
        }

        [Fact]
        public void GetProductShouldReturn420()
        {
            ProductController controller = CreateTestController();
            JsonResult actualResult = controller.GetProducts("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            JsonResult expectedResult = new JsonResult(new {error = "product not found"})
                {ContentType = "application/json", StatusCode = 420};
            Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
        }

        [Fact]
        public void CreateProductShouldCreateANewProduct()
        {
            Product newProduct = new Product()
            {
                Name = "pulcsi",
                DisplayName = "AKH Crewneck Pulóver",
                ImageName = "crewneck.jpg",
                Sizes = new List<SizeRecord>
                {
                    new SizeRecord() {Quantity = 3, Size = Size.XL}
                }
            };

            ProductController productController = CreateTestController();
            JsonResult actualResult = productController.CreateProduct(newProduct);


            JsonResult expectedResult = new JsonResult(newProduct)
            {
                ContentType = "application/json", StatusCode = 200
            };

            Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
        }

        [Fact]
        public void CreateNewProductWithSameSizeRecordReturn420()
        {
            Product newProduct = new Product()
            {
                Name = "pulcsi",
                DisplayName = "AKH Crewneck Pulóver",
                ImageName = "crewneck.jpg",
                Sizes = new List<SizeRecord>
                {
                    new SizeRecord() {Quantity = 3, Size = Size.XL},
                    new SizeRecord() {Quantity = 3, Size = Size.XL}
                }
            };

            ProductController productController = CreateTestController();
            JsonResult actualResult = productController.CreateProduct(newProduct);

            JsonResult expectedResult = new JsonResult(new {error = "couldn't create product"})
            {
                ContentType = "application/json", StatusCode = 420
            };

            Assert.Equal(expectedResult.ToString(), actualResult.ToString());
        }

        [Fact]
        public void UpdateProductShouldUpdateSuccessfully()
        {
            Product newProduct = new Product()
            {
                Name = "pulcsi",
                DisplayName = "AKH Crewneck Pulóver",
                ImageName = "crewneck.jpg",
                Sizes = new List<SizeRecord>
                {
                    new SizeRecord() {Quantity = 3, Size = Size.XL}
                }
            };

            ProductController productController = CreateTestController();
            JsonResult actualResult = productController.CreateProduct(newProduct);


            JsonResult expectedResult = new JsonResult(newProduct)
            {
                ContentType = "application/json", StatusCode = 200
            };

            Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());

            newProduct.Sizes.Add(new SizeRecord() {Quantity = 2, Size = Size.S});

            JsonResult newExpectedResult = new JsonResult(newProduct)
                {ContentType = "application/json", StatusCode = 200};
            JsonResult newActualResult = productController.UpdateProduct(newProduct);

            Assert.Equal(newExpectedResult.Value.ToString(), newActualResult.Value.ToString());
        }

        [Fact]
        public void UpdateProductShouldReturn420()
        {
            Product newProduct = new Product()
            {
                Name = "pulcsi",
                DisplayName = "AKH Crewneck Pulóver",
                ImageName = "crewneck.jpg",
                Sizes = new List<SizeRecord>
                {
                    new SizeRecord() {Quantity = 3, Size = Size.XL}
                }
            };

            ProductController productController = CreateTestController();
            JsonResult actualResult = productController.CreateProduct(newProduct);

            JsonResult expectedResult = new JsonResult(newProduct)
            {
                ContentType = "application/json", StatusCode = 200
            };

            Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());

            newProduct.Sizes.Add(new SizeRecord() {Quantity = 2, Size = Size.S});
            newProduct.Id = Guid.Empty;

            JsonResult newExpectedResult = new JsonResult(new {error = "couldn't create product"})
            {
                ContentType = "application/json", StatusCode = 420
            };

            JsonResult newActualResult = productController.UpdateProduct(newProduct);

            Assert.Equal(newExpectedResult.Value.ToString(), newActualResult.Value.ToString());
        }
    }
}